using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace PingChecker
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer pingTimer;
        private string logFilePath;
        private bool pinging;
        public int maxAcceptablePingTime;
        private const string configFileName = "pcconfig.txt";
        private int pingInterval = 1000;
        private int maxAcceptablePing = 500;
        private List<string> pingAddresses = new List<string> { "1.1.1.1", "8.8.8.8", "208.67.222.222" };
        private List<string> defaultPingAddresses = new List<string> { "1.1.1.1", "8.8.8.8", "208.67.222.222" };
        private List<string> routerIPAddresses = new List<string>();
        private int maxTextBoxLines = 100; // Максимальное количество строк в TextBox

        private CancellationTokenSource cancellationTokenSource;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private NotifyIcon notifyIcon;
        private Icon previousIcon;

        public Form1()
        {
            InitializeComponent();
            pingTimer = new System.Windows.Forms.Timer();
            pingTimer.Interval = pingInterval;
            pingTimer.Tick += async (sender, e) => await PingMultipleServersAsync(cancellationTokenSource.Token);

            notifyIcon = new NotifyIcon
            {
                Visible = true,
                Text = "Ping Checker"
            };

            Bitmap bitmap = new Bitmap(32, 32);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(Brushes.White, 0, 0, 32, 32);
            }
            previousIcon = Icon.FromHandle(bitmap.GetHicon());
            notifyIcon.Icon = previousIcon;
            this.Icon = previousIcon;
        }

        private async Task PingMultipleServersAsync(CancellationToken cancellationToken)
        {
            List<long> pingTimes = new List<long>();
            bool allTimedOut = true;

            foreach (string address in pingAddresses)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                using (Ping ping = new Ping())
                {
                    try
                    {
                        PingReply reply = await Task.Run(() => ping.Send(address, 1000), cancellationToken);
                        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {address} - {reply.Status} - Round-Trip Time: {reply.RoundtripTime} ms";
                        Invoke((MethodInvoker)delegate {
                            textBox1.AppendText(logEntry + Environment.NewLine);

                            if (textBox1.Lines.Length > maxTextBoxLines)
                            {
                                textBox1.Clear();
                                textBox1.AppendText("TextBox was cleared to free up memory." + Environment.NewLine);
                            }
                        });
                        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);

                        if (reply.Status == IPStatus.Success && defaultPingAddresses.Contains(address))
                        {
                            pingTimes.Add(reply.RoundtripTime);
                            allTimedOut = false;
                        }

                        bool isHighPing = reply.Status == IPStatus.Success && reply.RoundtripTime > maxAcceptablePingTime;

                        if (isHighPing && checkBox1.Checked)
                        {
                            PlayAlarmSound();
                        }

                        UpdateTaskbarIcon((int)reply.RoundtripTime, !isHighPing);
                    }
                    catch (OperationCanceledException)
                    {
                        // Операция была отменена, выход из цикла
                        break;
                    }
                    catch (Exception ex)
                    {
                        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {address} - Error: {ex.Message}";
                        Invoke((MethodInvoker)delegate {
                            textBox1.AppendText(logEntry + Environment.NewLine);

                            if (textBox1.Lines.Length > maxTextBoxLines)
                            {
                                textBox1.Clear();
                                textBox1.AppendText("TextBox was cleared to free up memory." + Environment.NewLine);
                            }
                        });
                        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                    }
                }
            }

            if (checkBoxRouterConnection.Checked)
            {
                foreach (string address in routerIPAddresses)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    using (Ping ping = new Ping())
                    {
                        try
                        {
                            PingReply reply = await Task.Run(() => ping.Send(address, 1000), cancellationToken);
                            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {address} - {reply.Status} - Round-Trip Time: {reply.RoundtripTime} ms";
                            Invoke((MethodInvoker)delegate {
                                textBox1.AppendText(logEntry + Environment.NewLine);

                                if (textBox1.Lines.Length > maxTextBoxLines)
                                {
                                    textBox1.Clear();
                                    textBox1.AppendText("TextBox was cleared to free up memory." + Environment.NewLine);
                                }
                            });
                            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);

                            if (reply.Status != IPStatus.Success)
                            {
                                string routerErrorLog = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: Router with IP {address} Not Responding!";
                                Invoke((MethodInvoker)delegate {
                                    textBox1.AppendText(routerErrorLog + Environment.NewLine);
                                });
                                File.AppendAllText(logFilePath, routerErrorLog + Environment.NewLine);

                                if (checkBox1.Checked) PlayAlarmSound();
                            }

                            bool isHighPing = reply.Status == IPStatus.Success && reply.RoundtripTime > maxAcceptablePingTime;

                            if (isHighPing && checkBox1.Checked)
                            {
                                PlayAlarmSound();
                            }

                            UpdateTaskbarIcon((int)reply.RoundtripTime, !isHighPing);
                        }
                        catch (OperationCanceledException)
                        {
                            // Операция была отменена, выход из цикла
                            break;
                        }
                        catch (Exception ex)
                        {
                            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: {address} - Error: {ex.Message}";
                            Invoke((MethodInvoker)delegate {
                                textBox1.AppendText(logEntry + Environment.NewLine);

                                if (textBox1.Lines.Length > maxTextBoxLines)
                                {
                                    textBox1.Clear();
                                    textBox1.AppendText("TextBox was cleared to free up memory." + Environment.NewLine);
                                }
                            });
                            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                        }
                    }
                }
            }

            if (!allTimedOut && pingTimes.Count > 0)
            {
                long averagePing = (long)pingTimes.Average();
                string averageLogEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}: Average Ping - {averagePing} ms";
                Invoke((MethodInvoker)delegate {
                    textBox1.AppendText(averageLogEntry + Environment.NewLine);

                    if (textBox1.Lines.Length > maxTextBoxLines)
                    {
                        textBox1.Clear();
                        textBox1.AppendText("TextBox was cleared to free up memory." + Environment.NewLine);
                    }

                    UpdateAppIcon(averagePing);
                });
                File.AppendAllText(logFilePath, averageLogEntry + Environment.NewLine);
            }
        }

        private void PlayAlarmSound()
        {
            System.Media.SystemSounds.Exclamation.Play();
        }

        private void UpdateAppIcon(long averagePing)
        {
            using (Bitmap bitmap = new Bitmap(32, 32))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);

                    using (Font font = new Font("Arial", 12, FontStyle.Bold))
                    {
                        StringFormat stringFormat = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        g.DrawString(averagePing.ToString(), font, Brushes.Black, new RectangleF(0, 0, 32, 32), stringFormat);
                    }
                }

                IntPtr hIcon = bitmap.GetHicon();
                Icon icon = Icon.FromHandle(hIcon);

                this.Icon = icon;
                notifyIcon.Icon = icon;

                if (previousIcon != null)
                {
                    DestroyIcon(previousIcon.Handle);
                    previousIcon.Dispose();
                }

                previousIcon = icon;
            }
        }

        private void UpdateTaskbarIcon(int milliseconds, bool isNormalPing)
        {
            // Реализация этого метода не требуется для текущего функционала.
        }

        private void buttonRouterIPList_Click(object sender, EventArgs e)
        {
            using (RouterIPListForm routerIPListForm = new RouterIPListForm(routerIPAddresses))
            {
                routerIPListForm.ShowDialog();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (pinging)
            {
                cancellationTokenSource.Cancel();
                pingTimer.Stop();
                btnStartStop.Text = "Start";
                pinging = false;
            }
            else
            {
                textBox1.Clear();
                string logFileName = $"ping_log_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, logFileName);

                cancellationTokenSource = new CancellationTokenSource();
                pingTimer.Start();
                btnStartStop.Text = "Stop";
                pinging = true;

                // Используем await для вызова асинхронного метода
                await PingMultipleServersAsync(cancellationTokenSource.Token);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int value, maxping;

            if (int.TryParse(intervaltb.Text, out value))
            {
                if (value > 0)
                {
                    pingTimer.Interval = value;
                }
            }

            if (int.TryParse(tbmaxping.Text, out maxping))
            {
                if (value > 0)
                {
                    maxAcceptablePingTime = maxping;
                }
            }

            try
            {
                pingInterval = int.Parse(intervaltb.Text);
                maxAcceptablePing = int.Parse(tbmaxping.Text);

                string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);

                using (StreamWriter sw = File.CreateText(configFilePath))
                {
                    sw.WriteLine($"PingInterval={pingInterval}");
                    sw.WriteLine($"AcceptablePing={maxAcceptablePing}");
                }

                MessageBox.Show("Configuration saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving configuration: " + ex.Message);
            }
        }

        private void intervaltb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfigValues();
            intervaltb.Text = pingInterval.ToString();
            tbmaxping.Text = maxAcceptablePing.ToString();

            pingTimer.Interval = int.Parse(intervaltb.Text);
            maxAcceptablePingTime = int.Parse(tbmaxping.Text);
        }

        private void LoadConfigValues()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);

            if (!File.Exists(configFilePath))
            {
                CreateDefaultConfigFile(configFilePath);
            }
            else
            {
                try
                {
                    string[] configLines = File.ReadAllLines(configFilePath);

                    foreach (string line in configLines)
                    {
                        if (line.StartsWith("PingInterval="))
                        {
                            if (int.TryParse(line.Replace("PingInterval=", ""), out int interval))
                            {
                                pingInterval = interval;
                            }
                        }
                        else if (line.StartsWith("AcceptablePing="))
                        {
                            if (int.TryParse(line.Replace("AcceptablePing=", ""), out int maxPing))
                            {
                                maxAcceptablePing = maxPing;
                            }
                        }
                        else if (line.StartsWith("RouterIP="))
                        {
                            string routerIP = line.Replace("RouterIP=", "").Trim();
                            routerIPAddresses.Add(routerIP);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error reading configuration file: " + ex.Message);
                }
            }
        }

        private void CreateDefaultConfigFile(string filePath)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine($"PingInterval={pingInterval}");
                    sw.WriteLine($"AcceptablePing={maxAcceptablePing}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating default configuration file: " + ex.Message);
            }
        }

        private void tbmaxping_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string keyName = "PingChecker";
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp != null)
            {
                rkApp.SetValue(keyName, Application.ExecutablePath);
                rkApp.Close();
            }

            MessageBox.Show("App has been added to autostart");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string keyName = "PingChecker";
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp != null)
            {
                rkApp.DeleteValue(keyName, false);
                rkApp.Close();
            }

            MessageBox.Show("App has been removed from autostart");
        }
    }
}
