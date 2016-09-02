using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MK.MobileDevice;
using System.Diagnostics;
using imobileDeviceiDevice;
using Microsoft.VisualBasic;


namespace iOS_Vzla
{
    public partial class Form1 : Form
    {
        iPhone aifon;
        Timer tem;
        iOSApplication apepes;



        public Form1()
        {
            Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            tem = new Timer();
            tem.Interval = 1500;
            tem.Tick += (s, e) => initPhone();
            tem.Start();
        }

        private void initPhone()
        {
            tem.Stop();
            try
            {
                aifon = new iPhone();
                aifon.Connect += Iph_Connect;
                aifon.Disconnect += Iph_Disconnect;
                label1.Text = "Esperando iPhone...";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Iph_Disconnect(object sender, ConnectEventArgs args)
        {
            ResetData();
        }

        private void ResetData()
        {
            this.label1.Text = "No hay dispositivo conectado";
        }

        private void Iph_Connect(object sender, ConnectEventArgs args)
        {
            if (aifon.IsConnected)
            {
                label1.Text = "Connectado a [" + aifon.DeviceProductType + "] " + aifon.DeviceName + " iOS " + aifon.ProductVersion;

                Dictionary<string, dynamic> infoBateria = aifon.RequestBatteryInfo();

                //iCloud Activo o no?
                if (aifon.FindMyiPhoneEnabled == true)
                {
                    label4.Text = "iCloud Activado";
                }
                else
                {
                    label4.Text = "iCloud Desactivado";
                }


                /////////////////////////////////////////////
                //      INFORMACION DEL DISPOSITIVO        //
                /////////////////////////////////////////////

                //Version de iOS
                versioniOS.Text = "Version " + aifon.DeviceVersion + "";


                //Serial de Placa 
                serialPlaca.Text = "[" + aifon.DeviceSerial + "]";


                //Numero de Telefono
                numeroTlf.Text = "N° " + aifon.DevicePhoneNumber + "";


                //IMEI
                imei.Text = "N° " + aifon.InternationalMobileEquipmentIdentity + "";


                //Version del Baseband
                versionBB.Text = "V " + aifon.DeviceBasebandVersion + "";


                //Operador
                if (aifon.IInternationalMobileSubscriberIdentity == "734047500395934")
                {
                    operador.Text = "Movistar VE";
                }








                //Tiene o no tiene Jailbreak
                if (aifon.IsJailbreak == true)
                {
                    JailbreaK.Text = "Tiene Jailbreak!!";
                }
                else
                {
                    JailbreaK.Text = "Sin Jailbreak!!";
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.twitter.com/VaasMontenegrox");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.reddit.com/Jailbreak");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.facebook.com/");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Buscar App que quieres instalar";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Archivo ipa (*.ipa)|*.ipa|Todos los archivos (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                aifon.InstallApplication(openFileDialog1.FileName);
            }
        }
    }
}
