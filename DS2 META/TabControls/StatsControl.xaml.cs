﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DS2S_META
{
    /// <summary>
    /// Interaction logic for StatsControl.xaml
    /// </summary>
    public partial class StatsControl : METAControl
    {
        public StatsControl()
        {
            InitializeComponent();
            foreach (DS2SClass charClass in DS2SClass.All)
                cmbClass.Items.Add(charClass);
            cmbClass.SelectedIndex = -1;
        }
        public void ReloadTab()
        {

        }
        private void cbmClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DS2SClass charClass = cmbClass.SelectedItem as DS2SClass;
            if (Hook.Loaded)
            {
                Hook.Class = charClass.ID;
                nudVig.Minimum = charClass.Vigor;
                nudEnd.Minimum = charClass.Endurance;
                nudVit.Minimum = charClass.Vitality;
                nudAtt.Minimum = charClass.Attunement;
                nudStr.Minimum = charClass.Strength;
                nudDex.Minimum = charClass.Dexterity;
                nudAdp.Minimum = charClass.Adaptability;
                nudInt.Minimum = charClass.Intelligence;
                nudFth.Minimum = charClass.Faith;
            }
        }

        internal override void UpdateCtrl()
        {
            
        }

        internal override void ReloadCtrl()
        {
            cmbClass.SelectedItem = cmbClass.Items.Cast<DS2SClass>().FirstOrDefault(c => c.ID == Hook.Class);
            txtName.Text = Hook.Name;
        }

        internal override void EnableCtrls(bool enable)
        {
            cmbClass.IsEnabled = enable;
            txtName.IsEnabled = enable;
            btnGive.IsEnabled = enable;
            btnResetSoulMemory.IsEnabled = enable;

            if (enable)
            {

            }
            else
            {
                cmbClass.SelectedIndex = -1;
            }
        }

        private void GiveSouls_Click(object sender, RoutedEventArgs e)
        {
            if (nudGiveSouls.Value.HasValue)
                Hook.GiveSouls(nudGiveSouls.Value.Value);
        }

        private void ResetSoulMemory_Click(object sender, RoutedEventArgs e)
        {
            Hook.ResetSoulMemory();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hook.GetItem(1, 60151000);
        }

        private void Name_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            Hook.Name = txtName.Text;
        }

    }
}
