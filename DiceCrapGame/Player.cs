using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceCrapGame
{
    class Player
    {
        //Parallel arrays
        public List<GroupBox> slots = new List<GroupBox>();
        private List<string> names = new List<string>();

        private List<TextBox> winCounters = new List<TextBox>();
        private List<int> wins = new List<int>();

        private List<TextBox> loseCounters = new List<TextBox>();
        private List<int> loses = new List<int>();

        private List<Label> diePointSign = new List<Label>();
        private List<TextBox> diePointCounters = new List<TextBox>();
        private List<int> diepoint = new List<int>();

        private List<bool> activeRound = new List<bool>();
        //

        private int currentPlayer = 0;

        public Player()
        {}

        public int getPlayerCount()
        { return slots.Count; }

        public void addPlayer(GroupBox playerSlot, string playerName, TextBox winTxt, TextBox losesTxt, TextBox diePointTxt, Label diePointLbl)
        {
            playerSlot.Text = playerName.ToString();
            names.Add(playerName);

            slots.Add(playerSlot);
            winCounters.Add(winTxt);
            wins.Add(0);
            loseCounters.Add(losesTxt);
            loses.Add(0);
            diePointSign.Add(diePointLbl);
            diePointCounters.Add(diePointTxt);
            diepoint.Add(0);

            activeRound.Add(true);

            //nextPlayer();
        }

        public int getDiePoint()
        { return diepoint[currentPlayer]; }

        public void diePointAchieved(int diePointVal)
        {
            diepoint[currentPlayer] = diePointVal;
            updateStats();
            diePointCounters[currentPlayer].Visible = true;
            diePointSign[currentPlayer].Visible = true;
        }
        public void playerWon()
        {
            wins[currentPlayer] += 1;
        }
        public void playerLose()
        {
            loses[currentPlayer] += 1;
            activeRound[currentPlayer] = false;
        }

        public void nextPlayer()
        {
            checkActivePlayers();

            diepoint[currentPlayer] = 0;
            diePointCounters[currentPlayer].Visible = false;
            diePointSign[currentPlayer].Visible = false;

            slots[currentPlayer].Enabled = false;

            updateStats();
            currentPlayer++;
            updateStats();
            
            if (!activeRound[currentPlayer]) { nextPlayer(); }
            slots[currentPlayer].Enabled = true;
        }

        private void updateStats()
        {
            if (currentPlayer >= slots.Count)
            { currentPlayer = 0; }

            winCounters[currentPlayer].Text = wins[currentPlayer].ToString();
            loseCounters[currentPlayer].Text = loses[currentPlayer].ToString();
            diePointCounters[currentPlayer].Text = diepoint[currentPlayer].ToString();
        }
        public int checkActivePlayers()
        {
            int totalActive = 0;
            for(int i = 0; i < activeRound.Count; i ++)
            {
                if (activeRound[i] == true) { totalActive++; }
            }
            if (totalActive<=0)
            {
                reactivateAllPlayers();
            }
            return totalActive;
        }
        private void reactivateAllPlayers()
        {
            for (int i = 0; i < activeRound.Count; i++)
            { activeRound[i] = true; }
        }

        public string getPlayerName() { return names[currentPlayer]; }
        public int getPlayerScore() { return wins[currentPlayer]; }
        public int getPlayerLoses() { return loses[currentPlayer]; }
    }
}
