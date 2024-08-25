using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PingChecker
{
    public partial class RouterIPListForm : Form
    {
        public List<string> RouterIPList { get; private set; }

        public RouterIPListForm(List<string> routerIPList)
        {
            InitializeComponent();
            RouterIPList = routerIPList;
            listBoxIPs.Items.AddRange(routerIPList.ToArray());
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxIP.Text))
            {
                RouterIPList.Add(textBoxIP.Text);
                listBoxIPs.Items.Add(textBoxIP.Text);
                textBoxIP.Clear();
                UpdateRouterIPsInConfig(); // Сохранить изменения в конфиг
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listBoxIPs.SelectedItem != null)
            {
                RouterIPList.Remove(listBoxIPs.SelectedItem.ToString());
                listBoxIPs.Items.Remove(listBoxIPs.SelectedItem);
                UpdateRouterIPsInConfig(); // Сохранить изменения в конфиг
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            RouterIPList.Clear();
            listBoxIPs.Items.Clear();
            UpdateRouterIPsInConfig(); // Сохранить изменения в конфиг
        }

        private void UpdateRouterIPsInConfig()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pcconfig.txt");

            // Сначала очищаем старые записи роутеров из конфига
            var configLines = File.ReadAllLines(configFilePath)
                                  .Where(line => !line.StartsWith("RouterIP="))
                                  .ToList();

            // Затем добавляем новые записи роутеров
            foreach (var ip in RouterIPList)
            {
                configLines.Add($"RouterIP={ip}");
            }

            // Перезаписываем конфигурационный файл
            File.WriteAllLines(configFilePath, configLines);
        }
    }
}
