using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Boolean continue_process = false;

        List<byte> list_inf = new List<byte>();

        String str_size_packet = "";
        String str_size_packet_OH = "";

        int size_path_packet = 0;
        int size_path_packet_OH = 0;

        int count_gl = 1;

        public int set_TextBox(List<byte> byte_to_Text, int count)
        {
            String all_str = "";
            all_str += count.ToString() + " фрагментированный пакет \n";
            foreach (var a in byte_to_Text)
            {
                all_str += Convert.ToString(a, 16).ToUpper();
            }
            all_str += "\n";
            Invoke(new Action(() => { richTextBox2.Text += all_str; }));
            return ++count;
        }

        private void выбратьфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {


                using (FileStream File = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    using (BinaryReader Br = new BinaryReader(File))
                    {

                        File.Seek(9, SeekOrigin.Begin);//9 байт "IP_STREAM"

                        //Считываем байты пока не достигнет конца файла
                        while (Br.BaseStream.Position != Br.BaseStream.Length)
                        {
                            //--------------------------------------------------------------------------------
                            byte[] list_ips = Br.ReadBytes(4);                                               //Заголовок IPS
                            File.Seek(10, SeekOrigin.Current);
                            byte[] list_gse = Br.ReadBytes(2);                                               //Заголовок GSE 2 байта которые определяют начало конец середину
                            String str_byte = BitConverter.ToString(list_gse, 0).Replace('-'.ToString(), "");//Перевод 2-ух байтов в строковое представление в HEX-е
                            int size_all_packet = BitConverter.ToInt32(list_ips, 0);                         //Размер всего пакета
                                                                                                             //--------------------------------------------------------------------------------

                            //Что делать если это начало пакета
                            if (str_byte[0] == '8')
                            {

                                str_size_packet = str_byte.Remove(0, 1);
                                size_path_packet = Convert.ToInt32(str_size_packet, 16) - 8;                    //8 байт 2-указатель на пакет и размер 6 ненужный заголовок
                                File.Seek(6, SeekOrigin.Current);                                               //Пропуск ненужных байт
                                list_inf.AddRange(Br.ReadBytes(size_path_packet));
                                File.Seek(4, SeekOrigin.Current);                                               //Пропуск ненужных байт

                                continue_process = true;

                            }
                            else if (str_byte[0] == '6' && continue_process)
                            {

                                str_size_packet_OH = str_byte.Remove(0, 1);
                                size_path_packet = Convert.ToInt32(str_size_packet_OH, 16) - 4;
                                list_inf.AddRange(Br.ReadBytes(size_path_packet - 2));
                                File.Seek(4, SeekOrigin.Current);
                                size_path_packet_OH += size_path_packet + 14;

                                while (size_all_packet != size_path_packet_OH)
                                {
                                    byte[] packet_OH;
                                    packet_OH = Br.ReadBytes(2);
                                    String str_byte_OH = BitConverter.ToString(packet_OH).Replace('-'.ToString(), "");
                                    str_size_packet_OH = "";
                                    size_path_packet_OH += 2;

                                    if (str_byte_OH[0] == 'C' && size_all_packet != size_path_packet_OH + 2)
                                    {
                                        str_size_packet_OH = str_byte_OH.Remove(0, 1);
                                        int size_prop = Convert.ToInt32(str_size_packet_OH, 16) - 2;

                                        if (size_path_packet_OH + size_prop + 4 == size_all_packet)
                                        {
                                            File.Seek(size_prop, SeekOrigin.Current);
                                            File.Seek(4, SeekOrigin.Current);
                                            size_path_packet_OH += size_prop + 4;
                                        }
                                        else
                                        {
                                            File.Seek(size_prop, SeekOrigin.Current);
                                            size_path_packet_OH += size_prop;
                                        }
                                    }
                                    else if (str_byte_OH[0] == '2' && size_all_packet != size_path_packet_OH + 2)
                                    {
                                        str_size_packet_OH = str_byte_OH.Remove(0, 1);
                                        int size_prop = Convert.ToInt32(str_size_packet_OH, 16) - 2;
                                        //File.Seek(size_prop, SeekOrigin.Current);
                                        list_inf.AddRange(Br.ReadBytes(size_prop));
                                        size_path_packet_OH += size_prop;
                                    }
                                    else if (str_byte_OH[0] == '8' && size_all_packet != size_path_packet_OH + 2)
                                    {
                                        str_size_packet_OH = str_byte_OH.Remove(0, 1);
                                        int size_prop = Convert.ToInt32(str_size_packet_OH, 16) - 2;
                                        //list_inf.Clear();
                                        list_inf.AddRange(Br.ReadBytes(size_prop));
                                        File.Seek(4, SeekOrigin.Current);
                                        continue_process = true;
                                        size_path_packet_OH += size_prop + 4;
                                        count_gl = set_TextBox(list_inf, count_gl);
                                    }
                                    else if (str_byte_OH[0] == '6' && size_all_packet != size_path_packet_OH + 2)
                                    {
                                        str_size_packet_OH = str_byte_OH.Remove(0, 1);
                                        size_path_packet = Convert.ToInt32(str_size_packet_OH, 16) - 2;
                                        if (size_path_packet_OH + size_path_packet + 4 == size_all_packet)
                                        {
                                            File.Seek(size_path_packet, SeekOrigin.Current);
                                            File.Seek(4, SeekOrigin.Current);//CRC-32
                                            size_path_packet_OH += size_path_packet + 4;
                                        }
                                        else
                                        {
                                            File.Seek(size_path_packet, SeekOrigin.Current);
                                            size_path_packet_OH += size_path_packet;
                                        }
                                    }
                                    else if (str_byte_OH[0] == '6' && size_all_packet != size_path_packet_OH + 2 && continue_process)
                                    {
                                        str_size_packet_OH = str_byte_OH.Remove(0, 1);
                                        size_path_packet = Convert.ToInt32(str_size_packet_OH, 16) - 2;
                                        if (size_path_packet_OH + size_path_packet + 4 == size_all_packet)
                                        {
                                            File.Seek(size_path_packet, SeekOrigin.Current);
                                            File.Seek(4, SeekOrigin.Current);//CRC-32
                                            size_path_packet_OH += size_path_packet + 4;
                                        }
                                        else
                                        {
                                            File.Seek(size_path_packet, SeekOrigin.Current);
                                            size_path_packet_OH += size_path_packet;
                                        }
                                    }
                                    else
                                    {
                                        File.Seek(2, SeekOrigin.Current);
                                        size_path_packet_OH += 2;
                                    }
                                }
                                continue_process = false;
                                size_path_packet_OH = 0;

                                if (list_inf.Count != 0)
                                {
                                    count_gl = set_TextBox(list_inf, count_gl);
                                    list_inf.Clear();
                                }

                            }
                            else
                            {
                                int size_else = BitConverter.ToInt32(list_ips, 0);
                                File.Seek(size_else - 12, SeekOrigin.Current);
                                continue_process = false;
                            }
                        }
                    }
                }


            }
            MessageBox.Show("Файл обработан!");
        }

        private void сохранитьКакIPSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog save_file = new SaveFileDialog();
            save_file.Title = "Выберете место, куда хотите сохранить файл: ";
            save_file.Filter = "files (*.ips)|*.ips";
            save_file.DefaultExt = ".ips";

            if (save_file.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox2.Text != "")
                {


                    Stream st = new FileStream(save_file.FileName, FileMode.Create);
                    List<string> stroki = new List<string>();
                    string str_save = "";
                    int digit = 0;
                    string IP_STREAM = "IP_STREAM";
                    byte[] bt;
                    stroki.AddRange(richTextBox2.Text.Split('\n'));

                    using (BinaryWriter bw = new BinaryWriter(st))
                    {
                        foreach (char str_ip in IP_STREAM)
                        {

                            bw.Write(Convert.ToByte(Convert.ToInt32(str_ip)));

                        }

                        for (int i = 1; i < stroki.Count; i += 2)
                        {
                            bt = BitConverter.GetBytes(Convert.ToInt32(stroki[i].Length) / 2);
                            bw.Write(bt);

                            for (int j = 0; j < stroki[i].Length - 1; j += 2)
                            {

                                str_save += $"{stroki[i][j]}{stroki[i][j + 1]}";
                                bw.Write(Convert.ToByte(Convert.ToInt32(str_save, 16)));
                                str_save = "";
                            }

                        }

                    }
                    MessageBox.Show("Файл сохранен!");
                }
                else
                {
                    MessageBox.Show("Вы пытаетесь сохранить нулевые данные!");
                }
            }
        }
    }
}
