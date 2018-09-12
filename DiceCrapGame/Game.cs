using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DiceCrapGame
{
    public partial class Game : Form
    {
        Die d;
        Player p = new Player();

        Button lastInsertClicked = new Button();

        List<string> winnerNames = new List<string>();
        List<int> winnerWins = new List<int>();
        List<int> winnerLose = new List<int>();

        const string path = "products.txt";

        public Game()
        {
            InitializeComponent();
            d = new Die(dieResultTxt, dOutcome1, dOutcome2);

            checkCanStartGame();

            slot1add.BringToFront();
            slot2add.BringToFront();
            slot3add.BringToFront();
            slot4add.BringToFront();
            slot5add.BringToFront();
            slot6add.BringToFront();
            startGame.BringToFront();

            //scoreboard.Items.Add("Bogallloo: Wins:20,Loses:22");

            //scoreboard.Items.Add("Gelloe");
        }

        private void rollDie_Click(object sender, EventArgs e)
        {
            d = new Die(dieResultTxt, dOutcome1, dOutcome2);
            if (p.getDiePoint() == 0) { Phase1(d.getResult()); }
            else { Phase2(d.getResult()); }
        }

        private void Phase1(int dieResult)
        {
            if (dieResult == 7 || dieResult == 11)
            {
                p.playerWon();
                checkRoundWinner();
                p.nextPlayer();
            }
            else if (dieResult == 2 || dieResult == 3 || dieResult == 12)
            {
                p.playerLose();
                p.nextPlayer();
            }
            else { p.diePointAchieved(dieResult); }
        }
        private void Phase2(int dieResult)
        {
            if (dieResult == p.getDiePoint())
            {
                p.playerWon();
                checkRoundWinner();
                p.nextPlayer();
            }
            else if (dieResult == 7)
            {
                p.playerLose();
                p.nextPlayer();
            }
        }

        public void checkRoundWinner()
        {
            if (p.checkActivePlayers()<=1)
            {
                bool userAlreadyExists = false;
                int existingUserIndex = 0;
                for (int i = 0; i < winnerNames.Count; i++)
                {
                    if (winnerNames[i]==p.getPlayerName())
                    {
                        userAlreadyExists = true;
                        existingUserIndex = i;
                        i = winnerNames.Count;
                    }
                    else { userAlreadyExists = false; }
                }
                if (userAlreadyExists)
                {
                    winnerNames.RemoveAt(existingUserIndex);
                    winnerWins.RemoveAt(existingUserIndex);
                    winnerLose.RemoveAt(existingUserIndex);

                    winnerNames.Add(p.getPlayerName());
                    winnerWins.Add(p.getPlayerScore());
                    winnerLose.Add(p.getPlayerLoses());
                }
                else
                {
                    winnerNames.Add(p.getPlayerName());
                    winnerWins.Add(p.getPlayerScore());
                    winnerLose.Add(p.getPlayerLoses());
                }
            }
            scoreboard.Clear();
            for (int i = winnerNames.Count-1; i >= 0; i--)
            { scoreboard.Items.Add(winnerNames[i]+": Wins:"+winnerWins[i]+",Loses:"+winnerLose[i]); }
        }

        private void slotAdd_Click(object sender, EventArgs e)
        {
            newPlayerNameInput.BringToFront();
            playerNameInput.Text = "";
            playerNameInput.Focus();

            lastInsertClicked = (Button)sender;
            lastInsertClicked.SendToBack();
        }

        private void cancelAddition_Click(object sender, EventArgs e)
        {
            newPlayerNameInput.SendToBack();
            lastInsertClicked.BringToFront();
            lastInsertClicked = new Button();
        }

        private void addPlayer_Click(object sender, EventArgs e)
        {
            if (lastInsertClicked.Name == "slot1add")
            { p.addPlayer(slot1, playerNameInput.Text, s1wins, s1loses, s1point, s1pointLbl); }
            else if (lastInsertClicked.Name == "slot2add")
            { p.addPlayer(slot2, playerNameInput.Text, s2wins, s2loses, s2point, s2pointLbl); }
            else if (lastInsertClicked.Name == "slot3add")
            { p.addPlayer(slot3, playerNameInput.Text, s3wins, s3loses, s3point, s3pointLbl); }
            else if (lastInsertClicked.Name == "slot4add")
            { p.addPlayer(slot4, playerNameInput.Text, s4wins, s4loses, s4point, s4pointLbl); }
            else if (lastInsertClicked.Name == "slot5add")
            { p.addPlayer(slot5, playerNameInput.Text, s5wins, s5loses, s5point, s5pointLbl); }
            else if (lastInsertClicked.Name == "slot6add")
            { p.addPlayer(slot6, playerNameInput.Text, s6wins, s6loses, s6point, s6pointLbl); }

            newPlayerNameInput.SendToBack();
            checkCanStartGame();
        }

        private void checkCanStartGame()
        {
            if (p.getPlayerCount() > 0) { startGame.Enabled = true; }
            startGame.Text = "Game Start!!!\n(" + p.getPlayerCount()+ ")";
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            p.slots[0].Enabled = true;
            slot1add.SendToBack();
            slot2add.SendToBack();
            slot3add.SendToBack();
            slot4add.SendToBack();
            slot5add.SendToBack();
            slot6add.SendToBack();
            startGame.SendToBack();

            slot1add.Enabled = false;
            slot2add.Enabled = false;
            slot3add.Enabled = false;
            slot4add.Enabled = false;
            slot5add.Enabled = false;
            slot6add.Enabled = false;
        }

        private void readSavedScores()
        {
            FileStream fs = null;
            StreamReader sr = null;

            //for reading line
            string readline;
            string[] parts;
            try
            {
                // open file for reading (the very first time, the file does not exist)
                fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                sr = new StreamReader(fs);
                //Reading data
                while (!sr.EndOfStream)//While theres still data
                {
                    readline = sr.ReadLine();
                    parts = readline.Split(',');//split where commas are
                    // store data in the arrays
                    winnerNames.Add(parts[0]);
                    winnerWins.Add(Convert.ToInt32(parts[1]));
                    winnerLose.Add(Convert.ToInt32(parts[2]));
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally { if (sr != null) sr.Close(); }
        }
        public void SaveScores()
        {
            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs);

                for (int i = 0; i<winnerNames.Count; i++)
                {
                    sw.WriteLine(winnerNames[i]+","+ winnerWins[i] + "," + winnerLose[i]);
                }

            }
            catch (Exception ex)
            { throw ex; }
            finally { if (sw != null) sw.Close(); }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            readSavedScores();
        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveScores();
        }
    }
}
