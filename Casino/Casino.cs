using MaterialSkin;
using MaterialSkin.Controls;
using Casino.Theme;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Casino {
    public partial class main : MaterialForm {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AllocConsole();

        private const int MinWidth = 1280;
        private const double AspectRatio = 16.0 / 9.0;

        List<Control> labels = new List<Control>();
        List<Control> smallLabels = new List<Control>();
        List<Control> bigLabels = new List<Control>();

        public main() {
            AllocConsole();

            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Colors.primary, Colors.primaryDark, Colors.primaryLight, Colors.accent, Colors.textShade);


            foreach (Control control in tabControl.Controls)
                foreach (Control childControl in control.Controls)
                    foreach (Control childChildControl in childControl.Controls)
                        foreach (Control childChildChildControl in childChildControl.Controls) {
                            if (childChildChildControl.Name.Contains("LabelSmall"))
                                smallLabels.Add(childChildChildControl);
                            else if (childChildChildControl.Name.Contains("LabelBig"))
                                bigLabels.Add(childChildChildControl);
                            else if (childChildChildControl.Name.Contains("Label"))
                                labels.Add(childChildChildControl);
                            foreach (Control childChildChildChildControl in childChildChildControl.Controls) {
                                if (childChildChildChildControl.Name.Contains("LabelSmall"))
                                    smallLabels.Add(childChildChildChildControl);
                                else if (childChildChildChildControl.Name.Contains("LabelBig"))
                                    bigLabels.Add(childChildChildChildControl);
                                else if (childChildChildChildControl.Name.Contains("Label"))
                                    labels.Add(childChildChildChildControl);
                            }
                        }

            formResize(this, null);

            this.Resize += new EventHandler(formResize);

            Blackjack blackjack = new Blackjack(tabControl.Controls[2].Controls[0]);
        }

        private void formResize(object sender, EventArgs e) {
            var form = sender as Form;
            int width = form.ClientRectangle.Width;

            int newWidth = Math.Max(form.ClientRectangle.Width, MinWidth);
            int newHeight = (int)(newWidth / AspectRatio);

            form.ClientSize = new Size(newWidth, newHeight);

            title.Font = new Font(title.Font.FontFamily, Math.Max(width / 35, MinWidth / 35), title.Font.Style);

            foreach (Control label in labels)
                label.Font = new Font(label.Font.FontFamily, Math.Max(this.ClientRectangle.Width / 80, MinWidth / 80), label.Font.Style);

            foreach (Control label in smallLabels)
                label.Font = new Font(label.Font.FontFamily, Math.Max(this.ClientRectangle.Width / 110, MinWidth / 110), label.Font.Style);

            foreach (Control label in bigLabels)
                label.Font = new Font(label.Font.FontFamily, Math.Max(this.ClientRectangle.Width / 50, MinWidth / 50), label.Font.Style);
        }
    }
}