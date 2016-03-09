using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PokedexNamespace;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;

namespace PokedexApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //The initialized list to which all Pokemon are a part of
        List<Pokemon> pokemans = new List<Pokemon>();

        //State at which list_value_changed will know when to do it's thang
        list_state searchstate;

        public enum list_state
        {
            NewList, OldList
        }
        
        //The current pokemon used to get info - VERY IMPORTANT
        Pokemon current;

        public MainWindow()
        {
            
            InitializeComponent();
            addPokesToList();
            PokeList.ItemsSource = pokemans;
        }
        
        /*Reading in all of the pokemon stored in data and initializing the master list*/
        public void addPokesToList()
        {
            StreamReader myIn = new StreamReader("PokemonList.txt.");
            string line;
            while ((line = myIn.ReadLine()) != null){
                pokemans.Add(new Pokemon(line, "Pictures/" + line + ".png"));
            }
            myIn.Dispose();
            searchstate = list_state.OldList;
        }

        /*Occurs whenever a list value is changed. All UI elements are rebound
        "searchstate" prevents the list from changing when a search is executed*/
        private void PokeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchstate == list_state.NewList)
            {
                searchstate = list_state.OldList;
            }
            else {
                current = PokeList.SelectedValue as Pokemon;

                binder_thang("ImageLocation", current, Image.SourceProperty, curr_image);
                poke_name.Text = current.Name;
                binder_thang("DexLabel", current, TextBlock.TextProperty, poke_index);
                binder_thang("Type1", current, TextBlock.TextProperty, poke_type1);
                binder_thang("Type2", current, TextBlock.TextProperty, poke_type2);
                binder_thang("Height", current, TextBlock.TextProperty, poke_height);
                binder_thang("Weight", current, TextBlock.TextProperty, poke_weight);
                binder_thang("Classification", current, TextBlock.TextProperty, poke_class);
                binder_thang("Description", current, TextBlock.TextProperty, description_text);
                set_stats(HP_stat, Attack_stat, Defense_stat, SpAtt_stat, SpDef_stat, Speed_stat);
                evo_panel_thang();
            }
        }

        /*A function to set the stat Textblocks to current pokemon*/
        private void set_stats(params TextBlock[] t)
        {
            for (int i = 0; i<t.Length; i++)
            {
                binder_thang(string.Format("Stats[{0}]", i), current, TextBlock.TextProperty, t[i]);
            }
        }

        /*A function to bind the evolution chart to current pokemon*/
        private void evo_panel_thang()
        {
            evo_layout.Children.Clear();
            foreach(int i in current.Evos)
            {
                if (i != 0)
                {
                    Image evo = new Image();
                    binder_thang("ImageLocation", pokemans[i - 1], Image.SourceProperty, evo);
                    evo_layout.Children.Add(evo);
                }
            }
        }

        /*Executes a query based on type
        Does not return the new list if there are no values in it*/
        private void make_query(string t)
        {
            var type = t;
            IEnumerable<Pokemon> p =  from x in pokemans
                                      where x.Type1 == t || x.Type2 == t
                                      select x;

            List<Pokemon> updatedList = new List<Pokemon>();
            foreach(Pokemon poke in p)
            {
                updatedList.Add(poke);
            }
            if (updatedList.Count != 0)
            {
                searchstate = list_state.NewList;
                PokeList.ItemsSource = updatedList;
            }
            else
                MessageBox.Show(string.Format("No values found for {0}", t));
        }
        
        /*Resets the current queried list back to the master list*/
        private void set_default_list()
        {
            searchstate = list_state.NewList;
            PokeList.ItemsSource = pokemans;
        }

        /*Bread and butter
        Binds a value to a UI element*/
        private void binder_thang(string paramater, object source, DependencyProperty dp, FrameworkElement fe)
        {
            Binding the_binder = new Binding();
            the_binder.Path = new PropertyPath(paramater);
            the_binder.Mode = BindingMode.OneTime;
            the_binder.Source = source;
            fe.SetBinding(dp, the_binder);
        }

        /*Search for a kind of type of pokemon
        Still needs dual type functionality*/
        private void search_button_Click(object sender, RoutedEventArgs e)
        {
            
            string type = search_value_text.Text;
            if
                (type == "") { MessageBox.Show("Cannot search for no type"); }
            else {
                make_query(type);
            }
        }

        /*Resets the list*/
        private void reset_button_Click(object sender, RoutedEventArgs e)
        {
            set_default_list();
        }
    }
    
}
