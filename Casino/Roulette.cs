using System;
using System.Windows.Forms;

public class Roulette {
    readonly int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
    readonly int[] blackNumbers = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
    readonly int[] firstDozen = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
    readonly int[] secondDozen = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
    readonly int[] thirdDozen = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
    readonly int timeToSpin = 250;

    private float angle = 0;
    private int ticks = 0;
    private int randomValue;
    private int bet = 100;
    private int money = 1000;
    private int total = 0;


    Label betLabel;
    Label moneyLabel;
    Label totalLabel;
    Label messageLabel;
    Label wheelLabel;
    Button spinButton;
    Button betUpButton;
    Button betDownButton;
    Button resetButton;
    PictureBox wheelPictureBox;
    System.Windows.Forms.Timer timer;
    Image image;

    List<BetType> bets = new List<BetType>();

    List<Button> actionButtons = new List<Button>();
    List<Button> numberButtons = new List<Button>();

    public Roulette(Control roulleteControl) {
        wheelPictureBox = roulleteControl.Controls.Find("WheelPictureBox", true).FirstOrDefault() as PictureBox;
        wheelPictureBox.Paint += Repaint;
        wheelPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        wheelPictureBox.BackgroundImageLayout = ImageLayout.Zoom;

        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1;
        timer.Tick += new EventHandler(TimerTick);

        image = Casino.Properties.Resources.roulette;

        betLabel = roulleteControl.Controls.Find("BetLabel", true).FirstOrDefault() as Label;
        betLabel.Text = "Bet: " + bet + "$";
        moneyLabel = roulleteControl.Controls.Find("MoneyLabel", true).FirstOrDefault() as Label;
        moneyLabel.Text = "Money: " + money + "$";
        totalLabel = roulleteControl.Controls.Find("TotalLabel", true).FirstOrDefault() as Label;
        totalLabel.Text = "Total: " + total + "$";
        wheelLabel = roulleteControl.Controls.Find("WheelLabel", true).FirstOrDefault() as Label;
        wheelLabel.Text = "Wheel: ?";
        messageLabel = roulleteControl.Controls.Find("MessageLabel", true).FirstOrDefault() as Label;
        messageLabel.Text = "Place your bet!";

        betUpButton = roulleteControl.Controls.Find("BetUpButton", true).FirstOrDefault() as Button;
        betUpButton.Click += BetUp;
        betDownButton = roulleteControl.Controls.Find("BetDownButton", true).FirstOrDefault() as Button;
        betDownButton.Click += BetDown;
        spinButton = roulleteControl.Controls.Find("SpinButton", true).FirstOrDefault() as Button;
        spinButton.Click += SpinTheWheel;
        resetButton = roulleteControl.Controls.Find("ResetButton", true).FirstOrDefault() as Button;
        resetButton.Click += Reset;

        spinButton.Enabled = false;

        Control boardTableLayout = roulleteControl.Controls.Find("BoardTableLayout", true).FirstOrDefault() as TableLayoutPanel;

        actionButtons.Add(boardTableLayout.Controls.Find("EvenButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("OddButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("RedButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("BlackButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("FirstButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("SecondButton", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Col1Button", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Col2Button", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Col3Button", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Row1Button", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Row2Button", true).FirstOrDefault() as Button);
        actionButtons.Add(boardTableLayout.Controls.Find("Row3Button", true).FirstOrDefault() as Button);

        foreach (Button button in actionButtons) {
            button.Click += (object sender, EventArgs e) => {
                BetType type;
                Button button = sender as Button;
                if (button.Name.Contains("even")) type = BetType.Even;
                else if (button.Name.Contains("odd")) type = BetType.Odd;
                else if (button.Name.Contains("red")) type = BetType.Red;
                else if (button.Name.Contains("black")) type = BetType.Black;
                else if (button.Name.Contains("first")) type = BetType.FirstHalf;
                else if (button.Name.Contains("second")) type = BetType.SecondHalf;
                else if (button.Name.Contains("col1")) type = BetType.FirstColumn;
                else if (button.Name.Contains("col2")) type = BetType.SecondColumn;
                else if (button.Name.Contains("col3")) type = BetType.ThirdColumn;
                else if (button.Name.Contains("row1")) type = BetType.FirstDozen;
                else if (button.Name.Contains("row2")) type = BetType.SecondDozen;
                else type = BetType.ThirdDozen;

                if (!bets.Contains(type)) {
                    if (total == money) return;
                    bets.Add(type);
                    button.BackgroundImage = Casino.Properties.Resources.grey;
                    total += bet;
                    totalLabel.Text = "Total: " + total + "$";

                    int auxTotal = bets.Count * (bet + 50);
                    if (auxTotal > money)
                        betUpButton.Enabled = false;
                } else {
                    bets.Remove(type);
                    button.BackgroundImage = Casino.Properties.Resources.green;
                    total -= bet;
                    totalLabel.Text = "Total: " + total + "$";

                    int auxTotal = bets.Count * (bet + 50);
                    if (auxTotal < money && bet < money / 2)
                        betUpButton.Enabled = true;

                    if (bets.Count == 0) spinButton.Enabled = false;
                }
                spinButton.Enabled = true;
            };
        }
        for (int i = 0; i < 37; i++)
            numberButtons.Add(boardTableLayout.Controls.Find("Button" + i, true).FirstOrDefault() as Button);
        foreach (Button button in numberButtons)
            button.Click += (object sender, EventArgs e) => {
                Button button = sender as Button;
                BetType type = NumberToType(Int32.Parse(button.Name.Substring(6)));

                if (bets.Contains(type)) {
                    bets.Remove(type);
                    if (blackNumbers.Contains(int.Parse(button.Name.Substring(6))))
                        button.BackgroundImage = Casino.Properties.Resources.black;
                    else if (redNumbers.Contains(int.Parse(button.Name.Substring(6))))
                        button.BackgroundImage = Casino.Properties.Resources.red;
                    total -= bet;
                    totalLabel.Text = "Total: " + total + "$";

                    int auxTotal = bets.Count * (bet + 50);
                    if (auxTotal < money && bet < money / 2)
                        betUpButton.Enabled = true;

                    if (bets.Count == 0) spinButton.Enabled = false;
                } else {
                    if (total == money) return;
                    bets.Add(type);
                    button.BackgroundImage = Casino.Properties.Resources.grey;
                    total += bet;
                    totalLabel.Text = "Total: " + total + "$";

                    int auxTotal = bets.Count * (bet + 50);
                    if (auxTotal > money)
                        betUpButton.Enabled = false;
                }
                spinButton.Enabled = true;
            };
    }

    private void BetUp(object sender, EventArgs e) {
        betDownButton.Enabled = true;
        bet += 50;
        int auxTotal = bets.Count * bet;
        if (auxTotal >= money) {
            if (auxTotal > money)
                bet -= 50;
            betUpButton.Enabled = false;
        } else if (bet >= money / 2) {
            bet = money / 2;
            betUpButton.Enabled = false;
        }
        total = bets.Count * bet;
        betLabel.Text = "Bet: " + bet + "$";
        totalLabel.Text = "Total: " + total + "$";
    }

    private void BetDown(object sender, EventArgs e) {
        betUpButton.Enabled = true;
        bet -= 50;
        if (bet <= 50) {
            betDownButton.Enabled = false;
            bet = 50;
        }
        total = bets.Count * bet;
        betLabel.Text = "Bet: " + bet + "$";
        totalLabel.Text = "Total: " + total + "$";
    }

    private void ResetRoulette() {
        foreach (Button button in actionButtons) {
            if (button.Name.Contains("red"))
                button.BackgroundImage = Casino.Properties.Resources.red;
            else if (button.Name.Contains("black"))
                button.BackgroundImage = Casino.Properties.Resources.black;
            else
                button.BackgroundImage = Casino.Properties.Resources.green;
        }
        foreach (Button button in numberButtons) {
            if (blackNumbers.Contains(int.Parse(button.Name.Substring(6))))
                button.BackgroundImage = Casino.Properties.Resources.black;
            else if (redNumbers.Contains(int.Parse(button.Name.Substring(6))))
                button.BackgroundImage = Casino.Properties.Resources.red;
        }
        numberButtons[0].BackgroundImage = Casino.Properties.Resources.green;
        bets.Clear();
        spinButton.Enabled = false;
        total = 0;
        totalLabel.Text = "Total: " + total + "$";
        moneyLabel.Text = "Money: " + money + "$";
        bet = 50;
        betLabel.Text = "Bet: " + bet + "$";
    }

    private void Reset(object sender, EventArgs e) {
        ResetRoulette();
        wheelLabel.Text = "Wheel: ?";
        messageLabel.Text = "Place your bet!";
    }

    private void SpinTheWheel(object sender, EventArgs e) {
        angle = -2;
        ticks = 0;
        spinButton.Enabled = false;
        betUpButton.Enabled = false;
        betDownButton.Enabled = false;
        resetButton.Enabled = false;
        randomValue = new Random().Next(0, 37);
        messageLabel.Text = "Spinning...";
        timer.Start();
    }

    private void TimerTick(object sender, EventArgs e) {
        if (ticks != timeToSpin)
            ticks++;
        angle += (timeToSpin + 25 - ticks) / 12;
        if (angle >= 360)
            angle = 0;
        wheelPictureBox.Invalidate();

        if (ticks == timeToSpin && angle >= 9.72 * randomValue - 2 && angle <= 9.72 * randomValue + 2) {
            int number = GetNumber();
            spinButton.Enabled = true;
            betUpButton.Enabled = true;
            betDownButton.Enabled = true;
            resetButton.Enabled = true;
            wheelLabel.Text = "Wheel: " + number;
            End(number);
            ResetRoulette();
            timer.Stop();
        }
    }

    private void Repaint(object sender, PaintEventArgs e) {
        Point center = new Point(wheelPictureBox.Width / 2, wheelPictureBox.Height / 2);
        e.Graphics.TranslateTransform(center.X, center.Y);
        e.Graphics.RotateTransform(angle);
        e.Graphics.DrawImage(image, -image.Width / 2, -image.Height / 2, image.Width, image.Height);
        e.Graphics.ResetTransform();
    }

    private void End(int number) {
        int totalWon = 0;

        if (blackNumbers.Contains(number) && bets.Contains(BetType.Black)) {
            bets.Remove(BetType.Black);
            totalWon += bet * 1;
        } else if (redNumbers.Contains(number) && bets.Contains(BetType.Red)) {
            bets.Remove(BetType.Red);
            totalWon += bet * 1;
        } else if (number % 2 == 0 && bets.Contains(BetType.Even)) {
            bets.Remove(BetType.Even);
            totalWon += bet * 1;
        } else if (number % 2 != 0 && bets.Contains(BetType.Odd)) {
            bets.Remove(BetType.Odd);
            totalWon += bet * 1;
        } else if (number >= 1 && number <= 18 && bets.Contains(BetType.FirstHalf)) {
            bets.Remove(BetType.FirstHalf);
            totalWon += bet * 1;
        } else if (number >= 19 && number <= 36 && bets.Contains(BetType.SecondHalf)) {
            bets.Remove(BetType.SecondHalf);
            totalWon += bet * 1;
        } else if (number >= 1 && number <= 12 && bets.Contains(BetType.FirstColumn)) {
            bets.Remove(BetType.FirstColumn);
            totalWon += bet * 2;
        } else if (number >= 13 && number <= 24 && bets.Contains(BetType.SecondColumn)) {
            bets.Remove(BetType.SecondColumn);
            totalWon += bet * 2;
        } else if (number >= 25 && number <= 36 && bets.Contains(BetType.ThirdColumn)) {
            bets.Remove(BetType.ThirdColumn);
            totalWon += bet * 2;
        } else if (bets.Contains(BetType.FirstDozen) && firstDozen.Contains(number)) {
            bets.Remove(BetType.FirstDozen);
            totalWon += bet * 2;
        } else if (bets.Contains(BetType.SecondDozen) && secondDozen.Contains(number)) {
            bets.Remove(BetType.SecondDozen);
            totalWon += bet * 2;
        } else if (bets.Contains(BetType.ThirdDozen) && thirdDozen.Contains(number)) {
            bets.Remove(BetType.ThirdDozen);
            totalWon += bet * 2;
        } else if (bets.Contains(NumberToType(number))) {
            bets.Remove(NumberToType(number));
            totalWon += bet * 35;
        }

        totalWon -= bet * bets.Count;
        money += totalWon;
        moneyLabel.Text = "Money: " + money + "$";

        if (totalWon > 0)
            messageLabel.Text = "You won " + totalWon + "$!";
        else if (totalWon == 0)
            messageLabel.Text = "You didn't win anything!";
        else
            messageLabel.Text = "You lost " + -totalWon + "$!";
    }

    private int GetNumber() {
        switch (randomValue) {
            case 0:
                return 0;
            case 1:
                return 26;
            case 2:
                return 3;
            case 3:
                return 35;
           case 4:
                return 12;
            case 5:
                return 28;
            case 6:
                return 7;
            case 7:
                return 29;
            case 8:
                return 18;
            case 9:
                return 22;
            case 10:
                return 9;
            case 11:
                return 31;
            case 12:
                return 14;
            case 13:
                return 20;
            case 14:
                return 1;
            case 15:
                return 33;
            case 16:
                return 16;
            case 17:
                return 24;
            case 18:
                return 5;
            case 19:
                return 10;
            case 20:
                return 23;
            case 21:
                return 8;
            case 22:
                return 30;
            case 23:
                return 11;
            case 24:
                return 36;
            case 25:
                return 13;
            case 26:
                return 27;
            case 27:
                return 6;
            case 28:
                return 34;
            case 29:
                return 17;
            case 30:
                return 25;
            case 31:
                return 2;
            case 32:
                return 21;
            case 33:
                return 4;
            case 34:
                return 19;
            case 35:
                return 15;
            case 36:
                return 32;
            default:
                return 0;
        }
    }

    private BetType NumberToType(int number) {
        switch (number) {
            case 0: return BetType.Zero;
            case 1: return BetType.One;
            case 2: return BetType.Two;
            case 3: return BetType.Three;
            case 4: return BetType.Four;
            case 5: return BetType.Five;
            case 6: return BetType.Six;
            case 7: return BetType.Seven;
            case 8: return BetType.Eight;
            case 9: return BetType.Nine;
            case 10: return BetType.Ten;
            case 11: return BetType.Eleven;
            case 12: return BetType.Twelve;
            case 13: return BetType.Thirteen;
            case 14: return BetType.Fourteen;
            case 15: return BetType.Fifteen;
            case 16: return BetType.Sixteen;
            case 17: return BetType.Seventeen;
            case 18: return BetType.Eighteen;
            case 19: return BetType.Nineteen;
            case 20: return BetType.Twenty;
            case 21: return BetType.TwentyOne;
            case 22: return BetType.TwentyTwo;
            case 23: return BetType.TwentyThree;
            case 24: return BetType.TwentyFour;
            case 25: return BetType.TwentyFive;
            case 26: return BetType.TwentySix;
            case 27: return BetType.TwentySeven;
            case 28: return BetType.TwentyEight;
            case 29: return BetType.TwentyNine;
            case 30: return BetType.Thirty;
            case 31: return BetType.ThirtyOne;
            case 32: return BetType.ThirtyTwo;
            case 33: return BetType.ThirtyThree;
            case 34: return BetType.ThirtyFour;
            case 35: return BetType.ThirtyFive;
            case 36: return BetType.ThirtySix;
            default: return BetType.Zero;
        }
    }

    private enum BetType {
        Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen, Seventeen, Eighteen, Nineteen, Twenty,
        TwentyOne, TwentyTwo, TwentyThree, TwentyFour, TwentyFive, TwentySix, TwentySeven, TwentyEight, TwentyNine, Thirty,
        ThirtyOne, ThirtyTwo, ThirtyThree, ThirtyFour, ThirtyFive, ThirtySix,
        Red,
        Black,
        Even,
        Odd,
        FirstHalf,
        SecondHalf,
        FirstColumn,
        SecondColumn,
        ThirdColumn,
        FirstDozen,
        SecondDozen,
        ThirdDozen,
    }

}
