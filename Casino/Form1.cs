using MaterialSkin;
using MaterialSkin.Controls;
using Casino.Theme;
using System.Windows.Forms;

namespace Casino {
    public partial class main : MaterialForm {
        private const int MinWidth = 768;
        private const double AspectRatio = 16.0 / 9.0;

        public main() {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Colors.primary, Colors.primaryDark, Colors.primaryLight, Colors.accent, Colors.textShade);
            title.Font = new Font("Roboto", Math.Max(this.ClientRectangle.Width / 25, MinWidth / 25), FontStyle.Bold);

            this.Resize += new EventHandler(Form_Resize);
        }

        private void Form_Resize(object sender, EventArgs e) {
            var form = sender as Form;
            int width = form.ClientRectangle.Width;

            int newWidth = Math.Max(form.ClientRectangle.Width, MinWidth);
            int newHeight = (int)(newWidth / AspectRatio);

            form.ClientSize = new Size(newWidth, newHeight);
            title.Font = new Font(title.Font.FontFamily, Math.Max(width / 25, MinWidth / 25), title.Font.Style);
        }
    }
}