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

namespace haromszogek
{
    public partial class frmFo : Form
    {
        private double aoldal;
        private double boldal;
        private double coldal;

        public frmFo()
        {
            aoldal = 0;
            boldal = 0;
            coldal = 0;
            InitializeComponent();
            tbAoldal.Text = aoldal.ToString();
            tbBoldal.Text = boldal.ToString();
            tbColdal.Text = coldal.ToString();
            lbHaromszogLista.Items.Clear();
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSzamol_Click(object sender, EventArgs e)
        {
            try
            {
                aoldal = Convert.ToDouble(tbAoldal.Text);
                boldal = Convert.ToDouble(tbBoldal.Text);
                coldal = Convert.ToDouble(tbColdal.Text);

                //StringBuilder szoveg = new StringBuilder();
                //szoveg.Append("a: ");
                //szoveg.Append(aoldal.ToString());
                //szoveg.Append("   b: ");
                //szoveg.Append(boldal.ToString());
                //szoveg.Append("   c: ");
                //szoveg.Append(coldal.ToString());

                if (aoldal == 0 || boldal == 0 || coldal == 0)
                {
                    MessageBox.Show("Nem lehet háromszög", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var h = new Haromszog(aoldal, boldal, coldal);
                    //if (h.Szerkesztheto)
                    //{
                    //    MessageBox.Show("Kerület: " + h.Kerulet + "  Terület: " + h.Terulet);

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Nem szerkeszthető belőle háromszög.");
                    //}

                    List<string> adatok = h.AdatokSzoveg();
                    foreach (var a in adatok)
                    {
                        lbHaromszogLista.Items.Add(a);
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Számot adj meg!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbAoldal.Focus();
            }
            
        }

        private void btnTorol_Click(object sender, EventArgs e)
        {
            if (lbHaromszogLista.Items.Count > 0)
            {
                lbHaromszogLista.Items.Clear();
            }
            else
            {
                MessageBox.Show("Nincs mit törölni.");
            }
            
        }

        private void btnFajlbol_Click(object sender, EventArgs e)
        {
            lbHaromszogLista.Items.Clear();
            if (ofdMegnyitas.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader file = new StreamReader(ofdMegnyitas.FileName);
                    try
                    {

                        while (!file.EndOfStream)
                        {
                            string sor = file.ReadLine();
                            var h = new Haromszog(sor);

                            lbHaromszogLista.Items.Add("Fájlból olvasás:");
                            foreach (var a in h.AdatokSzoveg())
                            {
                                lbHaromszogLista.Items.Add(a);
                            }
                            lbHaromszogLista.Items.Add("--------------------------------");
                        }

                        file.Close();
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        file.Close();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            } 
        }
    }
}
