using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training
{
    public partial class Drop : Form
    {
        public EnemyFactory Factory { get; set; }
        public Player Player1 { get; set; }

        public Drop()
        {
            InitializeComponent();

            // Najpierw trzeba stworzyc kilka przedmiotow
            // Normalnie by się nie tworzyło tego w kodzie, tylko np. ładowało listę z pliku
            Item item1 = new Item() { ItemName = "Pajęcza Włócznia", ItemType = ItemType.TwoHandedWeapon, Upgradable = true };
            Item item2 = new Item() { ItemName = "Szeroki Miecz", ItemType = ItemType.OneHandedWeapon, Upgradable = true };
            Item item3 = new Item() { ItemName = "Czerwona Mikstura (S)", ItemType = ItemType.Potion, Upgradable = false };
            Item item4 = new Item() { ItemName = "Niebieska Mikstura (S)", ItemType = ItemType.TwoHandedWeapon, Upgradable = true };
            Item item5 = new Item() { ItemName = "Żółć niedźwiedzia", ItemType = ItemType.UpgradeMaterial, Upgradable = false };
            Item item6 = new Item() { ItemName = "Wątroba wilka", ItemType = ItemType.UpgradeMaterial, Upgradable = false };

            // Teraz trzeba stworzyć kilka przeciwników (Listę, potrzebna jest w EnemyFactory)
            // To są jakby "szablony" przeciwnikow, EnemyFactory będzie tworzyła już konkretnego wroga

            List<Enemy> enemies = new List<Enemy>()
            {
                new Enemy("Brązowy niedźwiedź", new List<Item>() { item1, item2, item3, item5}),
                new Enemy("Czarny niedźwiedź", new List<Item>()  { item2, item3, item4, item5}),
                new Enemy("Głodny alfa wilk", new List<Item>() { item1, item2, item3, item6}),
                new Enemy("Głodny niebieski alfa wilk", new List<Item>() { item2, item3, item6})
            };

            // Teraz tylko trzeba stowrzyc fabrykę wrogów 

            Factory = new EnemyFactory() { AvaliableEnemies = enemies };

        }

        private void Drop_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Player1 != null)
            {
                Enemy enemy = Factory.GetRandomEnemy();
                richTextBox1.AppendText("\r\n" + enemy.Kill(Player1));
                richTextBox1.ScrollToCaret();

                // Odśwież ekwipunek

                richTextBox2.Text = "";

                Player1.Inventory.ForEach(s => richTextBox2.AppendText("\r\n" + s.GetDescription()));
                richTextBox2.ScrollToCaret();
            }
            else
            {
                richTextBox1.AppendText("\r\n Najpierw stwórz gracza, huncwocie!");
                richTextBox1.ScrollToCaret();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Player1 = new Player(textBox1.Text, 1);
        }
    }

    public enum ItemType
    {
        OneHandedWeapon,
        TwoHandedWeapon,
        UpgradeMaterial,
        Potion
    }

    public class Item
    {
        public string ItemName { get; set; }
        public ItemType ItemType { get; set; }
        // Can item be upgraded?
        public bool Upgradable { get; set; }

        public int UpgradeLevel { get; set; }

        public void Upgrade(int _upgradeLevel)
        {
            UpgradeLevel = _upgradeLevel;
        }

        public Item GetCopy()
        {
            return new Item() { ItemName = this.ItemName, ItemType = this.ItemType, Upgradable = this.Upgradable };
        }

        public string GetDescription()
        {
            string description = this.ItemName;

            if (Upgradable)
            {
                description += " +" + UpgradeLevel;
            }

            return description;
        }
    }

    public class Enemy
    {        
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public List<Item> CanDrop { get; set; }

        public Enemy(string _name, List<Item> _canDrop)
        {
            Name = _name;
            CanDrop = _canDrop;
            isAlive = true;
        }

        // Przekazywanie instancji gracza jako paramter pozowli potem dodać kolejnych graczy
        public string Kill(Player player)
        {
            //Ustaw przeciwnika jako martwego
            isAlive = false;
            Item drop = player.AddItem(GetRandomItem());

            return "Zabiłeś " + this.Name + " otrzymałeś: " + drop.GetDescription();
        }

        private Item GetRandomItem()
        {
            // Bo od zera się liczy
            int maxIndex = CanDrop.Count;
            Random rnd = new Random();
            // wez losowy item
            Item item = CanDrop[rnd.Next(0, maxIndex)].GetCopy();

            // sprawdz czy item moze byc ulepszony i jesli tak ulepsz do losowego plusa [0, 4]
            if (item.Upgradable)
            {
                // Zresetuj randa
                rnd = new Random();
                item.Upgrade(rnd.Next(0, 4));
            }

            return item;
        }

        // Zwraca kopię przeciwnika, potrzebne w EnemyFactory
        public Enemy GetCopy()
        {
            return new Enemy(this.Name, this.CanDrop);
        }
    }

    public class EnemyFactory
    {
        public List<Enemy> AvaliableEnemies { get; set; }

        public Enemy GetRandomEnemy()
        {
            Random rnd = new Random();

            return AvaliableEnemies[rnd.Next(0, AvaliableEnemies.Count)].GetCopy();
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(string _Name, int _level)
        {
            Name = _Name;
            Level = _level;

            Inventory = new List<Item>();
        }

        public Item AddItem(Item _item)
        {
            Inventory.Add(_item);
          
            return _item;
        }
    }
}
