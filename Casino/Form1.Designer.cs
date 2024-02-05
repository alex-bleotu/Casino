namespace Casino {
    partial class main {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            MenuImages = new ImageList(components);
            tabControl = new MaterialSkin.Controls.MaterialTabControl();
            Home = new TabPage();
            homeTableLayout = new TableLayoutPanel();
            title = new Label();
            Roullete = new TabPage();
            Blackjack = new TabPage();
            blackjackTableLayout = new TableLayoutPanel();
            blackjackPlayer2Panel = new Panel();
            materialButton2 = new MaterialSkin.Controls.MaterialButton();
            blackjackDealersPanel = new Panel();
            blackjackPlayer1Panel = new Panel();
            materialButton1 = new MaterialSkin.Controls.MaterialButton();
            blackjackPlayer3Panel = new Panel();
            blackjackPlayer4Panel = new Panel();
            blackjackCardsPanel = new Panel();
            blackjackActionPanel = new Panel();
            Machines = new TabPage();
            Shop = new TabPage();
            Settings = new TabPage();
            tabControl.SuspendLayout();
            Home.SuspendLayout();
            homeTableLayout.SuspendLayout();
            Blackjack.SuspendLayout();
            blackjackTableLayout.SuspendLayout();
            blackjackPlayer2Panel.SuspendLayout();
            blackjackPlayer1Panel.SuspendLayout();
            SuspendLayout();
            // 
            // MenuImages
            // 
            MenuImages.ColorDepth = ColorDepth.Depth32Bit;
            MenuImages.ImageStream = (ImageListStreamer)resources.GetObject("MenuImages.ImageStream");
            MenuImages.TransparentColor = Color.Transparent;
            MenuImages.Images.SetKeyName(0, "roullete.png");
            MenuImages.Images.SetKeyName(1, "settings.png");
            MenuImages.Images.SetKeyName(2, "chips.png");
            MenuImages.Images.SetKeyName(3, "dices.png");
            MenuImages.Images.SetKeyName(4, "machine.png");
            MenuImages.Images.SetKeyName(5, "cards.png");
            MenuImages.Images.SetKeyName(6, "home.png");
            // 
            // tabControl
            // 
            tabControl.Controls.Add(Home);
            tabControl.Controls.Add(Roullete);
            tabControl.Controls.Add(Blackjack);
            tabControl.Controls.Add(Machines);
            tabControl.Controls.Add(Shop);
            tabControl.Controls.Add(Settings);
            tabControl.Depth = 0;
            tabControl.Dock = DockStyle.Fill;
            tabControl.ImageList = MenuImages;
            tabControl.Location = new Point(3, 64);
            tabControl.MouseState = MaterialSkin.MouseState.HOVER;
            tabControl.Multiline = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1274, 653);
            tabControl.TabIndex = 0;
            // 
            // Home
            // 
            Home.BackColor = SystemColors.Window;
            Home.Controls.Add(homeTableLayout);
            Home.ImageKey = "home.png";
            Home.Location = new Point(4, 39);
            Home.Name = "Home";
            Home.Padding = new Padding(3);
            Home.Size = new Size(1266, 610);
            Home.TabIndex = 0;
            Home.Text = "Home";
            // 
            // homeTableLayout
            // 
            homeTableLayout.ColumnCount = 2;
            homeTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.53846F));
            homeTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.46154F));
            homeTableLayout.Controls.Add(title, 0, 0);
            homeTableLayout.Dock = DockStyle.Fill;
            homeTableLayout.Location = new Point(3, 3);
            homeTableLayout.Name = "homeTableLayout";
            homeTableLayout.RowCount = 3;
            homeTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 20.9580841F));
            homeTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 79.0419159F));
            homeTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            homeTableLayout.Size = new Size(1260, 604);
            homeTableLayout.TabIndex = 0;
            // 
            // title
            // 
            title.AutoSize = true;
            title.Dock = DockStyle.Fill;
            title.Font = new Font("Consolas", 24F, FontStyle.Bold, GraphicsUnit.Point);
            title.ForeColor = SystemColors.ControlText;
            title.Location = new Point(3, 0);
            title.Name = "title";
            title.Size = new Size(454, 122);
            title.TabIndex = 0;
            title.Text = "The Oasis";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Roullete
            // 
            Roullete.ImageKey = "roullete.png";
            Roullete.Location = new Point(4, 39);
            Roullete.Name = "Roullete";
            Roullete.Padding = new Padding(3);
            Roullete.Size = new Size(1266, 610);
            Roullete.TabIndex = 1;
            Roullete.Text = "Roullete";
            Roullete.UseVisualStyleBackColor = true;
            // 
            // Blackjack
            // 
            Blackjack.Controls.Add(blackjackTableLayout);
            Blackjack.ImageKey = "cards.png";
            Blackjack.Location = new Point(4, 39);
            Blackjack.Name = "Blackjack";
            Blackjack.Size = new Size(1266, 610);
            Blackjack.TabIndex = 2;
            Blackjack.Text = "Blackjack";
            Blackjack.UseVisualStyleBackColor = true;
            // 
            // blackjackTableLayout
            // 
            blackjackTableLayout.ColumnCount = 4;
            blackjackTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            blackjackTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            blackjackTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            blackjackTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            blackjackTableLayout.Controls.Add(blackjackPlayer2Panel, 1, 1);
            blackjackTableLayout.Controls.Add(blackjackDealersPanel, 1, 0);
            blackjackTableLayout.Controls.Add(blackjackPlayer1Panel, 0, 1);
            blackjackTableLayout.Controls.Add(blackjackPlayer3Panel, 2, 1);
            blackjackTableLayout.Controls.Add(blackjackPlayer4Panel, 3, 1);
            blackjackTableLayout.Controls.Add(blackjackCardsPanel, 3, 0);
            blackjackTableLayout.Controls.Add(blackjackActionPanel, 0, 0);
            blackjackTableLayout.Dock = DockStyle.Fill;
            blackjackTableLayout.Location = new Point(0, 0);
            blackjackTableLayout.Name = "blackjackTableLayout";
            blackjackTableLayout.RowCount = 2;
            blackjackTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            blackjackTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            blackjackTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            blackjackTableLayout.Size = new Size(1266, 610);
            blackjackTableLayout.TabIndex = 0;
            // 
            // blackjackPlayer2Panel
            // 
            blackjackPlayer2Panel.Controls.Add(materialButton2);
            blackjackPlayer2Panel.Dock = DockStyle.Fill;
            blackjackPlayer2Panel.Location = new Point(319, 308);
            blackjackPlayer2Panel.Name = "blackjackPlayer2Panel";
            blackjackPlayer2Panel.Size = new Size(310, 299);
            blackjackPlayer2Panel.TabIndex = 2;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.Dock = DockStyle.Bottom;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.Location = new Point(0, 263);
            materialButton2.Margin = new Padding(4, 6, 4, 6);
            materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = Color.Empty;
            materialButton2.Size = new Size(310, 36);
            materialButton2.TabIndex = 0;
            materialButton2.Text = "materialButton2";
            materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            // 
            // blackjackDealersPanel
            // 
            blackjackTableLayout.SetColumnSpan(blackjackDealersPanel, 2);
            blackjackDealersPanel.Dock = DockStyle.Fill;
            blackjackDealersPanel.Location = new Point(319, 3);
            blackjackDealersPanel.Name = "blackjackDealersPanel";
            blackjackDealersPanel.Size = new Size(626, 299);
            blackjackDealersPanel.TabIndex = 0;
            // 
            // blackjackPlayer1Panel
            // 
            blackjackPlayer1Panel.Controls.Add(materialButton1);
            blackjackPlayer1Panel.Dock = DockStyle.Fill;
            blackjackPlayer1Panel.Location = new Point(3, 308);
            blackjackPlayer1Panel.Name = "blackjackPlayer1Panel";
            blackjackPlayer1Panel.Size = new Size(310, 299);
            blackjackPlayer1Panel.TabIndex = 1;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.Dock = DockStyle.Bottom;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.Location = new Point(0, 263);
            materialButton1.Margin = new Padding(4, 6, 4, 6);
            materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(310, 36);
            materialButton1.TabIndex = 0;
            materialButton1.Text = "materialButton1";
            materialButton1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            // 
            // blackjackPlayer3Panel
            // 
            blackjackPlayer3Panel.Dock = DockStyle.Fill;
            blackjackPlayer3Panel.Location = new Point(635, 308);
            blackjackPlayer3Panel.Name = "blackjackPlayer3Panel";
            blackjackPlayer3Panel.Size = new Size(310, 299);
            blackjackPlayer3Panel.TabIndex = 3;
            // 
            // blackjackPlayer4Panel
            // 
            blackjackPlayer4Panel.Dock = DockStyle.Fill;
            blackjackPlayer4Panel.Location = new Point(951, 308);
            blackjackPlayer4Panel.Name = "blackjackPlayer4Panel";
            blackjackPlayer4Panel.Size = new Size(312, 299);
            blackjackPlayer4Panel.TabIndex = 4;
            // 
            // blackjackCardsPanel
            // 
            blackjackCardsPanel.Dock = DockStyle.Fill;
            blackjackCardsPanel.Location = new Point(951, 3);
            blackjackCardsPanel.Name = "blackjackCardsPanel";
            blackjackCardsPanel.Size = new Size(312, 299);
            blackjackCardsPanel.TabIndex = 5;
            // 
            // blackjackActionPanel
            // 
            blackjackActionPanel.Dock = DockStyle.Fill;
            blackjackActionPanel.Location = new Point(3, 3);
            blackjackActionPanel.Name = "blackjackActionPanel";
            blackjackActionPanel.Size = new Size(310, 299);
            blackjackActionPanel.TabIndex = 6;
            // 
            // Machines
            // 
            Machines.ImageKey = "machine.png";
            Machines.Location = new Point(4, 39);
            Machines.Name = "Machines";
            Machines.Size = new Size(1266, 610);
            Machines.TabIndex = 3;
            Machines.Text = "Machines";
            Machines.UseVisualStyleBackColor = true;
            // 
            // Shop
            // 
            Shop.ImageKey = "chips.png";
            Shop.Location = new Point(4, 39);
            Shop.Name = "Shop";
            Shop.Size = new Size(1266, 610);
            Shop.TabIndex = 5;
            Shop.Text = "Shop";
            Shop.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            Settings.ImageKey = "settings.png";
            Settings.Location = new Point(4, 39);
            Settings.Name = "Settings";
            Settings.Size = new Size(1266, 610);
            Settings.TabIndex = 4;
            Settings.Text = "Settings";
            Settings.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1280, 720);
            Controls.Add(tabControl);
            DrawerShowIconsWhenHidden = true;
            DrawerTabControl = tabControl;
            ForeColor = SystemColors.ControlText;
            Name = "main";
            Text = "The Oasis";
            tabControl.ResumeLayout(false);
            Home.ResumeLayout(false);
            homeTableLayout.ResumeLayout(false);
            homeTableLayout.PerformLayout();
            Blackjack.ResumeLayout(false);
            blackjackTableLayout.ResumeLayout(false);
            blackjackPlayer2Panel.ResumeLayout(false);
            blackjackPlayer2Panel.PerformLayout();
            blackjackPlayer1Panel.ResumeLayout(false);
            blackjackPlayer1Panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ImageList MenuImages;
        private MaterialSkin.Controls.MaterialTabControl tabControl;
        private TabPage Home;
        private TabPage Roullete;
        private TabPage Blackjack;
        private TabPage Machines;
        private TabPage Shop;
        private TabPage Settings;
        private TableLayoutPanel homeTableLayout;
        private Label title;
        private TabPage home;
        private TabPage roullete;
        private TabPage blackjack;
        private TabPage machines;
        private TabPage shop;
        private TableLayoutPanel blackjackTableLayout;
        private Panel blackjackDealersPanel;
        private Panel blackjackPlayer2Panel;
        private Panel blackjackPlayer1Panel;
        private Panel blackjackPlayer3Panel;
        private Panel blackjackPlayer4Panel;
        private Panel blackjackCardsPanel;
        private Panel blackjackActionPanel;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialButton materialButton2;
    }
}