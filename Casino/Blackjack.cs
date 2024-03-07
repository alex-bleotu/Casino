using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Casino.Theme;
using MaterialSkin.Controls;
using static Cards;

public class Blackjack
{
	public const int startingMoney = 1000;
	public const int startingBet = 50;

	public class Player {
		public bool isPlaying { get; set; } = false;
		public int money { get; set; } = startingMoney;
		public int bet { get; set; } = startingBet;
		public int handValue { get; set; } = 0;
		public bool isBusted { get; set; } = false;
		public bool hasAce { get; set; } = false;

		public Button joinButton { get; set; }
		public Button hitButton { get; set; }
		public Button standButton { get; set; }
		public Button doubleButton { get; set; }
		public Button leaveButton { get; set; }
		public List<MaterialButton> cards { get; set; } = new List<MaterialButton>();
		public Control buttonsTable { get; set; }
		public Label label { get; set; }
		public Label moneyLabel { get; set; }
		public Label betLabel { get; set; }
	}

	List<Player> players = new List<Player>();
	Button startButton = new Button();
	Label houseLabel = new Label();
	List<MaterialButton> houseCards = new List<MaterialButton>();
	Cards cards;

	int currentPlayersTurn = 0;
	int houseHandValue = 0;
	bool hasHouseAce = false;

	public Blackjack(Control blackJackControl) {
		cards = new Cards();

		players.Add(new Player());
		players.Add(new Player());
		players.Add(new Player());
		players.Add(new Player());

		startButton = blackJackControl.Controls.Find("StartButton", true).FirstOrDefault() as Button;
		startButton.Click += new EventHandler(StartButton_Click);
		startButton.Enabled = false;

		houseLabel = blackJackControl.Controls.Find("HouseLabelBig", true).FirstOrDefault() as Label;

		houseCards.Add(blackJackControl.Controls.Find("HouseCard1", true).FirstOrDefault() as MaterialButton);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard2", true).FirstOrDefault() as MaterialButton);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard3", true).FirstOrDefault() as MaterialButton);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard4", true).FirstOrDefault() as MaterialButton);

		foreach (Control control in blackJackControl.Controls)
			if (control.Name.Contains("player"))
				foreach (Control childControl in control.Controls) {
					int index = int.Parse(control.Name.Substring(6, 1)) - 1;

					if (childControl.Name.Contains("JoinButton")) {
						players[index].joinButton = childControl as Button;
						childControl.Click += new EventHandler(JoinButton_Click);
					} else if (childControl.Name.Contains("ButtonTable")) {
						players[index].buttonsTable = childControl;
						players[index].hitButton = childControl.Controls[1] as Button;
						players[index].standButton = childControl.Controls[0] as Button;
						players[index].doubleButton = childControl.Controls[2] as Button;
						players[index].leaveButton = childControl.Controls[3] as Button;
						players[index].buttonsTable.Visible = false;
						players[index].hitButton.Enabled = false;
						players[index].standButton.Enabled = false;
						players[index].doubleButton.Enabled = false;
						players[index].hitButton.Click += new EventHandler(HitButton_Click);
						players[index].standButton.Click += new EventHandler(StandButton_Click);
						players[index].doubleButton.Click += new EventHandler(DoubleButton_Click);
						players[index].leaveButton.Click += new EventHandler(LeaveButton_Click);
					} else if (childControl.Name.Contains("Label"))
						players[index].label = childControl as Label;
					else if (childControl.Name.Contains("Cards"))
						foreach (Control cardControl in childControl.Controls) {
							if (cardControl.Name.Contains("Card"))
								players[index].cards.Add(cardControl as MaterialButton);
							else if (cardControl.Name.Contains("MoneyLabel"))
								players[index].moneyLabel = cardControl as Label;
							else if (cardControl.Name.Contains("BetLabel"))
								players[index].betLabel = cardControl as Label;
						}
				}
	}

	private void JoinButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        players[index].isPlaying = true;
		players[index].joinButton = button;
		players[index].joinButton.Visible = false;
		players[index].buttonsTable.Visible = true;
		players[index].money = startingMoney;
		players[index].bet = startingBet;
		players[index].label.Text = "Player " + (index + 1);
		players[index].moneyLabel.Text = ("Money: " + (players[index].money.ToString() + "$"));
		players[index].betLabel.Text = "Bet: " + players[index].bet + "$";
		button.Visible = false;

        startButton.Enabled = true;
    }

	private void HitButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
		int i;
		for (i = players[index].cards.Count - 1; i >= 0; i--)
			if (players[index].cards[i].Visible == false) {
				AddPlayerCard(index, i);
				break;
			}
		if (i == 0 && players[index].handValue < 21)
			NextRound();
    }

	private void StandButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        NextRound();
    }

	private void DoubleButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
		players[index].bet *= 2;
		players[index].betLabel.Text = "Bet: " + players[index].bet;
        for (int i = players[index].cards.Count - 1; i >= 0; i--)
            if (players[index].cards[i].Visible == false) {
                AddPlayerCard(index, i);
                break;
            }
    }

	private void LeaveButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
		players[index].isPlaying = false;
        players[index].buttonsTable.Visible = false;
        players[index].joinButton.Visible = true;
		players[index].label.Text = "Player " + (index + 1);
		players[index].money = startingMoney;
		players[index].bet = startingBet;
		players[index].isBusted = false;
		players[index].cards.ForEach(card => card.Visible = false);
		players[index].joinButton.Enabled = true;
		players[index].moneyLabel.Text = "";
		players[index].betLabel.Text = "";
        players[index].hasAce = false;
        players[index].label.ForeColor = Color.White;
        players[index].handValue = 0;
        if (currentPlayersTurn == index)
            NextRound();
	}

	private void NextRound() {
		players[currentPlayersTurn].buttonsTable.Visible = false;
		players[currentPlayersTurn].hitButton.Enabled = false;
		players[currentPlayersTurn].standButton.Enabled = false;
		players[currentPlayersTurn].doubleButton.Enabled = false;
		if (players[currentPlayersTurn].handValue != 21 && !players[currentPlayersTurn].isBusted)
            players[currentPlayersTurn].label.Text = "Player " + (currentPlayersTurn + 1);
		int counter = 0;
		while (true && counter < 4) {
            currentPlayersTurn++;
            if (currentPlayersTurn == players.Count) {
				EndGame();
				return;
			}
            if (players[currentPlayersTurn].isPlaying && !players[currentPlayersTurn].isBusted && players[currentPlayersTurn].handValue < 21)
                break;
			counter++;
        }
		if (counter < 4) {
			players[currentPlayersTurn].hitButton.Enabled = true;
			players[currentPlayersTurn].standButton.Enabled = true;
			players[currentPlayersTurn].doubleButton.Enabled = true;
			players[currentPlayersTurn].label.Text = "Player " + (currentPlayersTurn + 1) + " - Turn";
		}
    }

	private void EndGame() {
		AddHouseCard(0);
		if (houseHandValue < 17)
            AddHouseCard(2);
        if (houseHandValue < 17)
			AddHouseCard(3);

        if (hasHouseAce && houseHandValue + 10 <= 21)
            houseHandValue += 10;

		if (houseHandValue == 21) {
			houseLabel.Text = "House - Blackjack";
			houseLabel.ForeColor = Colors.accent;
		}

		int winners = 0;

        if (houseHandValue > 21) {
			houseLabel.Text = "House - Busted";
			for (int i = 0; i < players.Count; i++) {
                if (players[i].isPlaying) {
					if (players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Busted";
						players[i].money -= players[i].bet;
					} else if (players[i].handValue == 21 || players[i].hasAce && players[i].handValue == 11) {
						players[i].label.Text = "Player " + (i + 1) + " - Blackjack";
						players[i].label.ForeColor = Colors.accent;
						players[i].money += players[i].bet * 2;
                        winners++;
                    } else {
						players[i].label.Text = "Player " + (i + 1) + " - Winner";
						players[i].money += players[i].bet;
						players[i].label.ForeColor = Colors.accent;
                        winners++;
                    }
                    players[i].moneyLabel.Text = "Money: " + players[i].money + "$";
                }
            }
		} else {
			houseLabel.Text = "House";
			for (int i = 0; i < players.Count; i++) {
				if (players[i].isPlaying) {
					if (players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Busted";
						players[i].money -= players[i].bet;
					}

					if (players[i].handValue + 10 <= 21 && players[i].hasAce)
						players[i].handValue += 10;

                    if (players[i].handValue == 21 || players[i].hasAce && players[i].handValue == 11) {
                        players[i].label.Text = "Player " + (i + 1) + " - Blackjack";
                        players[i].label.ForeColor = Colors.accent;
                        players[i].money += players[i].bet * 2;
                        winners++;
                    } else if (players[i].handValue > houseHandValue && !players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Winner";
						players[i].money += players[i].bet;
						players[i].label.ForeColor = Colors.accent;
                        winners++;
                    } else if (players[i].handValue == houseHandValue && !players[i].isBusted)
						players[i].label.Text = "Player " + (i + 1) + " - Draw";
					else if (!players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Loser";
						players[i].money -= players[i].bet;
					}
				}
				players[i].moneyLabel.Text = "Money: " + players[i].money + "$";
			}
		}

		if (winners == 0) {
			houseLabel.Text = "House - Winner";
			houseLabel.ForeColor = Colors.accent;
		}

		for (int i = 0; i < players.Count; i++) {
			players[i].joinButton.Enabled = true;
			if (players[i].money <= 0) {
				players[i].joinButton.Visible = true;
				players[i].buttonsTable.Visible = false;
				players[i].cards[0].Visible = false;
				players[i].cards[1].Visible = false;
				players[i].cards[2].Visible = false;
				players[i].cards[3].Visible = false;
				players[i].label.Text = "Player " + (i + 1) + " - Bankrupt";
            }
		}

		startButton.Enabled = true;
	}

	private void StartButton_Click(object sender, EventArgs e) {
		startButton.Enabled = false;
		hasHouseAce = false;
		houseHandValue = 0;
		houseLabel.Text = "House";
		houseLabel.ForeColor = Color.White;
		houseCards[0].Visible = false;
		houseCards[1].Visible = false;
		houseCards[2].Visible = false;
		houseCards[3].Visible = false;

        currentPlayersTurn = 0;

        for (int index = 0; index < players.Count; index++) {
			players[index].joinButton.Enabled = false;
			for (int i = 0; i < players[index].cards.Count; i++)
				players[index].cards[i].Visible = false;
			players[index].handValue = 0;
			players[index].isBusted = false;
			players[index].label.ForeColor = Color.White;
			players[index].label.Text = "Player " + (index + 1);
			players[index].moneyLabel.Text = "Money: " + players[index].money + "$";
			players[index].bet = startingBet;
			players[index].betLabel.Text = "Bet: " + players[index].bet + "$";
			players[index].hasAce = false;

			if (players[index].isPlaying) {
				players[index].buttonsTable.Visible = true;
				AddPlayerCard(index, 3);
				AddPlayerCard(index, 2);
            }
        }

		houseCards[0].Visible = true;
		houseCards[0].Text = "";
		houseCards[0].Icon = null;
		AddHouseCard(1);

        for (int i = 0; i < players.Count; i++) {
            if (players[i].isPlaying) {
                currentPlayersTurn = i;
                if (players[i].handValue == 21) {
                    NextRound();
                    return;
                }
                players[i].hitButton.Enabled = true;
				players[i].standButton.Enabled = true;
				players[i].doubleButton.Enabled = true;
				players[i].label.Text = "Player " + (i + 1) + " - Turn";
				break;
            }
        }	
    }

	private void AddHouseCard(int cardIndex) {
        Card card = cards.NextCard();
		if (card.value == 1) {
			houseCards[cardIndex].Text = "A";
			hasHouseAce = true;
		} else if (card.value < 11)
			houseCards[cardIndex].Text = card.value.ToString();
		else
			switch (card.value) {
				case 11:
					houseCards[cardIndex].Text = "J";
					break;
				case 12:
					houseCards[cardIndex].Text = "Q";
					break;
				case 13:
					houseCards[cardIndex].Text = "K";
					break;
			}
        switch (card.suit) {
            case Suit.Clubs:
				houseCards[cardIndex].Icon = Casino.Properties.Resources.clubs;
                break;
            case Suit.Hearts:
				houseCards[cardIndex].Icon = Casino.Properties.Resources.heart;
                break;
            case Suit.Spades:
				houseCards[cardIndex].Icon = Casino.Properties.Resources.spade;
                break;
            case Suit.Diamonds:
				houseCards[cardIndex].Icon = Casino.Properties.Resources.diamond;
                break;
        }
		houseCards[cardIndex].Visible = true;
		if (card.value > 10)
			houseHandValue += 10;
		else
			houseHandValue += card.value;
    }

	private void AddPlayerCard(int playerIndex, int cardIndex) {
		if (cardIndex != 0) {
			Card card;
            do {
                card = cards.TryCard();
            } while (players[playerIndex].hasAce && card.value == 1);
			cards.AddCard(card);
			if (card.value == 1) {
				players[playerIndex].cards[cardIndex].Text = "A";
				players[playerIndex].hasAce = true;
			} else if (card.value < 11)
				players[playerIndex].cards[cardIndex].Text = card.value.ToString();
			else
				switch (card.value) {
					case 11:
						players[playerIndex].cards[cardIndex].Text = "J";
						break;
					case 12:
						players[playerIndex].cards[cardIndex].Text = "Q";
						break;
					case 13:
						players[playerIndex].cards[cardIndex].Text = "K";
						break;
				}
			switch (card.suit) {
				case Suit.Clubs:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.clubs;
					break;
				case Suit.Hearts:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.heart;
					break;
				case Suit.Spades:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.spade;
					break;
				case Suit.Diamonds:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.diamond;
					break;
			}
			players[playerIndex].cards[cardIndex].Visible = true;
			if (card.value > 10)
				players[playerIndex].handValue += 10;
            else
				players[playerIndex].handValue += card.value;

			if (players[playerIndex].handValue > 21)
				players[playerIndex].isBusted = true;

            if (players[playerIndex].isBusted) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Busted";
				players[playerIndex].buttonsTable.Visible = false;
                if (cardIndex < 2)
					NextRound();
            } else if (players[playerIndex].handValue == 21 || players[playerIndex].hasAce && players[playerIndex].handValue == 11) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Blackjack";
				players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.ForeColor = Colors.accent;
                players[playerIndex].handValue = 21;
                if (cardIndex < 2)
                    NextRound();
            }
        } else {
			Card card;
			do {
				card = cards.TryCard();
				if (card.value > 6)
					break;
			} while (players[playerIndex].handValue + card.value < 21);
			cards.AddCard(card);

			if (card.value == 1) {
				players[playerIndex].cards[cardIndex].Text = "A";
				players[playerIndex].hasAce = true;
			} else if (card.value < 11)
				players[playerIndex].cards[cardIndex].Text = card.value.ToString();
			else
				switch (card.value) {
					case 11:
						players[playerIndex].cards[cardIndex].Text = "J";
						break;
					case 12:
						players[playerIndex].cards[cardIndex].Text = "Q";
						break;
					case 13:
						players[playerIndex].cards[cardIndex].Text = "K";
						break;
				}
            switch (card.suit) {
                case Suit.Clubs:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.clubs;
                    break;
                case Suit.Hearts:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.heart;
                    break;
                case Suit.Spades:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.spade;
                    break;
                case Suit.Diamonds:
					players[playerIndex].cards[cardIndex].Icon = Casino.Properties.Resources.diamond;
                    break;
            }
			players[playerIndex].cards[cardIndex].Visible = true;
            if (card.value > 10)
                players[playerIndex].handValue += 10;
            else
                players[playerIndex].handValue += card.value;

            if (players[playerIndex].handValue > 21)
				players[playerIndex].isBusted = true;

            if (players[playerIndex].isBusted) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Busted";
                if (cardIndex < 2)
                    NextRound();
            } else if (players[playerIndex].handValue == 21 || players[playerIndex].hasAce && players[playerIndex].handValue == 11) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Blackjack";
				players[playerIndex].label.ForeColor = Colors.accent;
				players[playerIndex].handValue = 21;
                if (cardIndex < 2)
                    NextRound();
            }
        }
	}
}
