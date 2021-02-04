using DPUruNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPCtlUruNet;
using System.Drawing.Imaging;
using SISPE_MIGRACION.formularios.NOMINAS.SUPERVIVENCIA;

namespace SISPE_MIGRACION.codigo.herramientas.forms.huella
{
    delegate void metodo(string cadena);
    public partial class frmEnrollar : Form
    {
        internal metodo enviar { get; set; }
        private EnrollmentControl _enrollmentControl {get;set;}
        public bool cerrar;
        public frmEnrollar()
        {
            InitializeComponent();
        }

        private void frmEnrollar_Load(object sender, EventArgs e)
        {
      

            _enrollmentControl = new DPCtlUruNet.EnrollmentControl(globales.dispositivo, Constants.CapturePriority.DP_PRIORITY_COOPERATIVE);
        _enrollmentControl.BackColor = SystemColors.Window;
            _enrollmentControl.Location = new System.Drawing.Point(3, 3);
            _enrollmentControl.Name = "ctlEnrollmentControl";
            _enrollmentControl.Size = new System.Drawing.Size(482, 346);
            _enrollmentControl.TabIndex = 0;
            _enrollmentControl.OnCancel += new DPCtlUruNet.EnrollmentControl.CancelEnrollment(this.enrollment_OnCancel);
            _enrollmentControl.OnCaptured += new DPCtlUruNet.EnrollmentControl.FingerprintCaptured(this.enrollment_OnCaptured);
            _enrollmentControl.OnDelete += new DPCtlUruNet.EnrollmentControl.DeleteEnrollment(this.enrollment_OnDelete);
            _enrollmentControl.OnEnroll += new DPCtlUruNet.EnrollmentControl.FinishEnrollment(this.enrollment_OnEnroll);
            _enrollmentControl.OnStartEnroll += new DPCtlUruNet.EnrollmentControl.StartEnrollment(this.enrollment_OnStartEnroll);


            panel1.Controls.Add(_enrollmentControl);
        }

        private void enrollment_OnStartEnroll(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            //throw new NotImplementedException();
        }

        private void enrollment_OnEnroll(EnrollmentControl enrollmentControl, DataResult<Fmd> enrollmentResult, int fingerPosition)
        {
            if (enrollmentResult != null && enrollmentResult.Data != null)
            {
                Fmd datos = enrollmentResult.Data;
                string cadena = Fmd.SerializeXml(datos);
                enviar(cadena);
                this.Close();


                
            }

        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void enrollment_OnDelete(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            throw new NotImplementedException();
        }

        private void enrollment_OnCaptured(EnrollmentControl enrollmentControl, CaptureResult captureResult, int fingerPosition)
        {
            

            if (captureResult.ResultCode != Constants.ResultCode.DP_SUCCESS)
            {
                if (globales.dispositivo != null)
                {
                    globales.dispositivo.Dispose();
                    globales.dispositivo = null;
                }

                // Disconnect reader from enrollment control
                _enrollmentControl.Reader = null;

                MessageBox.Show("Error:  " + captureResult.ResultCode);
              //  btnCancel.Enabled = false;
            }
            else
            {
                if (captureResult.Data != null)
                {
                    foreach (Fid.Fiv fiv in captureResult.Data.Views)
                    {
                        imagenDedo.Image = CreateBitmap(fiv.RawImage, fiv.Width, fiv.Height);
                    }
                }
            }
        }


        public Bitmap CreateBitmap(byte[] bytes, int width, int height)
        {
            byte[] rgbBytes = new byte[bytes.Length * 3];

            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                rgbBytes[(i * 3)] = bytes[i];
                rgbBytes[(i * 3) + 1] = bytes[i];
                rgbBytes[(i * 3) + 2] = bytes[i];
            }
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            for (int i = 0; i <= bmp.Height - 1; i++)
            {
                IntPtr p = new IntPtr(data.Scan0.ToInt64() + data.Stride * i);
                System.Runtime.InteropServices.Marshal.Copy(rgbBytes, i * bmp.Width * 3, p, bmp.Width * 3);
            }

            bmp.UnlockBits(data);

            return bmp;
        }

        private void enrollment_OnCancel(EnrollmentControl enrollmentControl, Constants.ResultCode result, int fingerPosition)
        {
            throw new NotImplementedException();
        }

        private void frmEnrollar_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmEnrollar_FormClosing(object sender, FormClosingEventArgs e)
        {
            _enrollmentControl.Refresh();
            this.cerrar = true;
           // frmSupervivencia super = new frmSupervivencia(cerrar);

        }
    }
}
