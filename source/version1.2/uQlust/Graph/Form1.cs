using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using uQlustCore.Distance;
using uQlustCore;
using uQlustCore.Profiles;
using System.Runtime.InteropServices;
//using System.Timers;

namespace Graph
{

        
    public partial class Form1 : Form
    {
        string lastPath="";
        Options opt=new Options();
        JobManager manager = new JobManager();
        List<string> directories = new List<string>();
        List<Best5> best5Res = new List<Best5>();        
        List<InternalProfileBase> profiles=new List<InternalProfileBase>();

        UpdateMessageWindow upMessage = new UpdateMessageWindow();

        private void UpdateProgress(object sender, EventArgs e)
        {
            
        //    int xx=(int)(manager.ProgressUpdate()*100);
            //dataGridView1.Rows[0].Cells[6].Value = xx;


            Dictionary<string,double> res=manager.ProgressUpdate();

            if (res == null)
            {
               // TimeInterval.Stop();
                return;
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                if (res.ContainsKey((string)dataGridView1.Rows[i].Cells[0].Value))
                    dataGridView1.Rows[i].Cells[6].Value =(int)( res[(string)dataGridView1.Rows[i].Cells[0].Value]*100);

        }

        public Form1()
        {
            FragBagProfile ggg;
            
            //ggg = new FragBagProfile();
           // ggg.ReadLibrary();            
            InitializeComponent();
            //ProfileAutomatic.AnalyseProfileFile("C:\\tmp\\1abv_dssProfile.native",SIMDIST.SIMILARITY);
            InternalProfilesManager.RemoveProfilesFile();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
/*            dataGridView1.Columns.Add(new ProgressColumn());
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            //dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            ProgressColumn Progress =(ProgressColumn)dataGridView1.Columns[dataGridView1.Columns.Count - 1];
            Progress.DefaultCellStyle = dataGridViewCellStyle1;
            Progress.HeaderText = "Progress [%]";
            Progress.Name = "Progress";
            //Progress.ProgressBarColor = System.Drawing.Color.Lime;
            Progress.ReadOnly = true;*/

            System.OperatingSystem osinfo=System.Environment.OSVersion;
            Console.WriteLine("system: " + osinfo.VersionString+" "+Environment.Is64BitOperatingSystem);
            profiles=InternalProfilesManager.InitProfiles();
            /*if (osinfo.VersionString.Contains("Win"))
            {
                if (Environment.Is64BitOperatingSystem)
                    profiles.Add(new DsspInternalProfile());
            }
            else
                profiles.Add(new DsspInternalProfile());
            profiles.Add(new ContactProfile());
            profiles.Add(new UserDefinedProfile());
            profiles.Add(new CAProfiles());
            profiles.Add(new ContactMapProfile());
            profiles.Add(new ContactMapProfileRNA());
            profiles.Add(new FragBagProfile());*/

            manager.beginJob = AddItemToList;
            manager.updateJob = UpdateListBox;
            manager.message = ErrorMessage;

            TimeInterval.InitTimer(UpdateProgress);
/*            this.toolsToolStripMenuItem.DropDownItems.Remove(this.best5ToolStripMenuItem);
            this.toolsToolStripMenuItem.DropDownItems.Remove(this.compareBest5ToolStripMenuItem);
            this.toolsToolStripMenuItem.DropDownItems.Remove(this.best5RmsdCenterToolStripMenuItem);*/


            /*string[] dirs = Directory.GetDirectories("Y:\\casp10","T065*");
            foreach (var item in dirs)
            {
                string[] Files = Directory.GetFiles(item);
                List<string> ddd = new List<string>(Files);
               

                Rmsd rmsd = new Rmsd(ddd, "", false, PDB.PDBMODE.ONLY_CA);
                List <string> kk =new List<string>();
                foreach(var nn in Files)
                {
                    string[] aux = nn.Split(Path.DirectorySeparatorChar);
                    kk.Add(aux[aux.Length - 1]);
                }
                string []aa=item.Split(Path.DirectorySeparatorChar);
                rmsd.DistStat(kk,aa[aa.Length-1]);
            }*/
           /* List <string> ddd=new List<string>();
            ddd.Add("y:\\TASSER_decoys\\1af7.pdb");
            ddd.Add("y:\\TASSER_decoys\\1af7\\d1216.pdb");
            DistanceMeasure maxsub = new MaxSub(ddd, "", false);

            double cc = maxsub.GetDistance("1af7.pdb","d1216.pdb");*/

            try
            {
                manager.opt.ReadDefaultFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            

            foreach (var item in manager.opt.dataDir)
                listBox1.Items.Add((string)item);

            foreach (var item in manager.opt.dcdFiles)
                listBox1.Items.Add(item);

            foreach (var item in manager.opt.profileFiles)
                listBox1.Items.Add(item+" profiles");

            if (manager.opt.profileFiles.Count > 0)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Checked = true;
            }

            SetDataDirOptions();
            Settings setCheck = new Settings();
            
            try
            {
                setCheck.Load();
            }
            catch
            {
                MessageBox.Show("First you have to set options!");
                SetSettings(false);
            }
            label4.Text = setCheck.mode.ToString();
        }
        private void UpBest5List(Best5 item)
        {
            best5Res.Remove(item);
        }
        private void DisableTab_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage page = tabControl.TabPages[e.Index];
            if (!page.Enabled)
            {
                //Draws disabled tab
                using (SolidBrush brush = new SolidBrush(SystemColors.GrayText))
                {
                    e.Graphics.DrawString(page.Text, page.Font, brush, e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
            else
            {
                // Draws normal tab
                using (SolidBrush brush = new SolidBrush(page.ForeColor))
                {
                    e.Graphics.DrawString(page.Text, page.Font, brush, e.Bounds.X + 3, e.Bounds.Y + 3);
                }
            }
            
        }
        public delegate void RemoveListBoxItem(string item,bool errorFlag);

        void ErrorMessage(string message)
        {
            MessageBox.Show(message);
        }

        public bool CheckSettings()
        {
            return true;
        }
        public delegate void AddListBoxItem(string item, string clType, string dirName, DistanceMeasures measure);        
        
        public void AddItemToList(string item,string clType,string dirName,DistanceMeasures measure)
        {
            int num;
            if (dataGridView1.InvokeRequired)
                dataGridView1.Invoke(new AddListBoxItem(AddItemToList), new object[] { item, clType, dirName, measure });
            else
            {

                dataGridView1.Rows.Add(1);
                num = dataGridView1.Rows.Count-1;
                dataGridView1.Rows[num].DefaultCellStyle.ForeColor = Color.Red;
                dataGridView1.Rows[num].Cells[0].Value = item;
                dataGridView1.Rows[num].Cells[1].Value = clType;
                dataGridView1.Rows[num].Cells[2].Value = measure;
                dataGridView1.Rows[num].Cells[3].Value = dirName;
            }

        }
        private void CloseVisual(string item)
        {
        }
        private void AnCluster(List<ClusterOutput> clOut, string name)
        {
            ClusterStat stat = new ClusterStat(clOut[0], name);
            stat.Show();
        }
        private void ClassifyClusters(List<ClusterOutput> clOut, string name)
        {
            ClusterClassification classCl = new ClusterClassification(clOut[0]);
            classCl.Text = name;
            classCl.Show();
        }


        private void UpdateListBox(string item,bool errorFlag)
        {

            if (dataGridView1.InvokeRequired)
                dataGridView1.Invoke(new RemoveListBoxItem(UpdateListBox), new object[] { item, errorFlag });
            else
            {
                TimeInterval.Stop();
                if (ErrorBase.GetErrors().Count > 0)
                    toolStripButton1.Enabled = true;

                if (manager.clOutput.Count != 0)
                {
                    UpdateProgress(null, null);
                    TimeSpan span = DateTime.Now.Subtract(DateTime.Now);
                    if (manager.clOutput.ContainsKey(item))
                    {
                        int remIndex = 0;
                        //                        vis = new ClusterGraphVis(manager.clOutput[item]);
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            if ((string)dataGridView1.Rows[i].Cells[0].Value == item)
                            {
                                dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Green;
                                dataGridView1.Rows[i].Cells[4].Value = manager.clOutput[item].time;
                                dataGridView1.Rows[i].Cells[1].Value = manager.clOutput[item].clusterType;
                                dataGridView1.Rows[i].Cells[2].Value = manager.clOutput[item].measure;
                                dataGridView1.Rows[i].Cells[5].Value = manager.clOutput[item].peekMemory;
                                dataGridView1.Rows[i].Cells[6].Value = 100;
                                remIndex = i;
                            }
                    }
                }
                else
                {
                    

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                            dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                            dataGridView1.Rows[i].Cells[4].Value = "Error";
                            dataGridView1.Rows[i].Cells[5].Value = "Error";
                    }

                }
            }
        }
        
        private void SetDataDirOptions()
        {
            if (listBox1.Items.Count> 0)
            {
                manager.opt.dataDir.Clear();
                manager.opt.dcdFiles.Clear();
                manager.opt.profileFiles.Clear();

                foreach (var item in listBox1.Items)
                {
                    if((item.ToString()).Contains(" DCD "))
                         manager.opt.dcdFiles.Add((DCDFile)item);
                    else
                        if (item.ToString().Contains(" profiles"))
                        {
                            string tmp = item.ToString();
                            tmp=tmp.Replace(" profiles", "");
                            manager.opt.profileFiles.Add(tmp);
                        }
                        else
                            manager.opt.dataDir.Add((string)item);
                }
                lastPath = listBox1.Items[listBox1.Items.Count - 1].ToString();
                if (lastPath.Contains(" DCD "))
                {
                    
                    string[] aux = lastPath.Split(new string[]{ " DCD " }, StringSplitOptions.None);
                    lastPath = aux[0];
                }
                if (lastPath.Contains(" profiles"))
                {

                    string[] aux = lastPath.Split(new string[] { " profiles" }, StringSplitOptions.None);
                    lastPath = aux[0];
                }

                hierarchicalToolStripMenuItem.Enabled = true;
                kmeansToolStripMenuItem.Enabled = true;
                thresholdToolStripMenuItem.Enabled = true;
                hushClusterToolStripMenuItem.Enabled = true;
            }

        }   
        private void dataDirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res;
            if (lastPath.Length > 0)
                folderBrowserDialog1.SelectedPath = lastPath;            
            res=folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                if(!listBox1.Items.Contains(folderBrowserDialog1.SelectedPath))
                    listBox1.Items.Add(folderBrowserDialog1.SelectedPath);
                SetDataDirOptions();
            }

        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetOptions()
        {
            //jury.Checked=false;
            //rmsd.Checked=false;
            //maxsub.Checked=false;
      
        }
        private void Run()
        {
            string processName = "";
            bool end = false;
            toolStripButton1.Enabled = false;
            ErrorBase.ClearErrors();
            SetDataDirOptions();
            do
            {

               

                GetProcessName gName = new GetProcessName(opt.clusterAlgorithm.ToString());
                gName.ShowDialog();

                if (gName.DialogResult == DialogResult.OK)
                {
                    List<string> keys = new List<string>(manager.clOutput.Keys);
                    Dictionary<string, int> dic = new Dictionary<string, int>();

                    foreach (var item in keys)
                    {
                        string []aux = item.Split('_');
                        string l="";

                        for (int i = 0; i < aux.Length - 1; i++)
                            l += aux[i];
                        if(!dic.ContainsKey(l))                        
                            dic.Add(l,1);
                    }
                    
                    if (dic.ContainsKey(gName.name))                    
                        MessageBox.Show("Process name: " + gName.name + " already exists");
                    else
                    {
                        processName = gName.name;
                        end = true;
                    }
                }
                else
                    return;
            }
            while (!end);
            manager.mUpdate = upMessage;
            try
            {
                TimeInterval.Start();
                manager.RunJob(processName);
            }
            catch (Exception ex)
            {
                TimeInterval.Stop();
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
       
        private void SetSettings(bool flag)
        {
            FormSettings set = null;
            try
            {
                set = new FormSettings(flag);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return;
            }            
            set.ShowDialog();

        }

        private void setingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSettings(true);
            
            Settings setCheck = new Settings();            
            setCheck.Load();
            label4.Text = setCheck.mode.ToString();
            InternalProfilesManager.RemoveProfilesFile();
            foreach (var item in profiles)
            {
                item.ClearProfiles();
                item.LoadProfiles();
                if (item.GetMode().Contains(setCheck.mode))
                    item.AddInternalProfiles();
            }

        }


        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            menuClick();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Config file (*.cfg)|*.cfg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                manager.opt.SaveOptions(saveFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Results file (*.cres)|*.cres";
            DialogResult res=saveFileDialog1.ShowDialog();
            if(res==DialogResult.OK)
                if(saveFileDialog1.FileName.Length>0)
                    manager.SaveOutput(saveFileDialog1.FileName+".cres");            
        }
        private void UpdateGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in manager.clOutput.Keys)
            {
                int num;
                dataGridView1.Rows.Add(1);
                num = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[num].DefaultCellStyle.ForeColor = Color.Green;
                dataGridView1.Rows[num].Cells[0].Value = manager.clOutput[item].name;
                dataGridView1.Rows[num].Cells[1].Value = manager.clOutput[item].clusterType;
                dataGridView1.Rows[num].Cells[2].Value = manager.clOutput[item].measure;                
                dataGridView1.Rows[num].Cells[4].Value = manager.clOutput[item].time;
                dataGridView1.Rows[num].Cells[3].Value = manager.clOutput[item].dirName;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Results (.cres)|*.cres|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult res=openFileDialog1.ShowDialog();
            if(res==DialogResult.OK)
                if (openFileDialog1.FileName.Length > 0)
                {
                    manager.LoadOutput(openFileDialog1.FileName);
                    UpdateGridView();
                }
        }
        private void menuClick()                 
        {
             if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DefaultCellStyle.ForeColor==Color.Green)
            {
                 DataGridViewCellCollection cell=dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells;
                 string name = (string)cell[0].Value;
                 //if(cell[cell.Count-1].Value!=null)
                   // name = (string)cell[cell.Count-1].Value;

                if(manager.clOutput.ContainsKey(name))
                {
                        ClusterGraphVis vis = new ClusterGraphVis(manager.clOutput[name],name);
                        vis.Closing = CloseVisual;
                        vis.SClusters(name, dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[2].Value.ToString(),null);
                    
                }
             }
        }
        private void gotoProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuClick();
        }


        private void LoadMenuItem(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Config file (*.cfg)|*.cfg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                manager.opt.ReadOptionFile(openFileDialog1.FileName);
                SetOptions();
            }
        }

        private void resultsComparisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClusterComparision compWin = new ClusterComparision(manager.clOutput);

            compWin.ShowDialog();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (dataGridView1.Rows.Count!= 0)
            {
                manager.RemoveJob((string)dataGridView1.Rows[dataGridView1.Rows.Count-1].Cells[0].Value);

                if (manager.clOutput.ContainsKey((string)dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value))
                    manager.clOutput.Remove((string)dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value);
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }

        }
        private bool CheckProfilesExist()
        {
            foreach (var item in listBox1.Items)
            {
                if (item.ToString().Contains(" profiles"))
                    return true;
            }
            return false;
        }
        private void hierarchicalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = CheckProfilesExist();
            HierarchicalCForm hForm;
            if(manager.opt.clusterAlgorithm!=null && manager.opt.clusterAlgorithm.Count>0)
                hForm = new HierarchicalCForm(manager.opt.hierarchical,manager.opt.clusterAlgorithm[0],flag,GetAlignedProfiles());            
            else
                hForm = new HierarchicalCForm(manager.opt.hierarchical, ClusterAlgorithm.uQlustTree, flag,GetAlignedProfiles());            
            DialogResult res=hForm.ShowDialog();

            if (res == DialogResult.OK)
            {
                manager.opt.hierarchical = hForm.localOpt;
                manager.opt.clusterAlgorithm.Clear();
                manager.opt.clusterAlgorithm.Add(hForm.alg);
                
                Run();
            }

        }    

        private void kmeansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = CheckProfilesExist();
            KMeansForm km = new KMeansForm(manager.opt.kmeans,flag,GetAlignedProfiles());

            DialogResult res = km.ShowDialog();
            if (res == DialogResult.OK)
            {
                manager.opt.kmeans = km.localObj;
                manager.opt.clusterAlgorithm.Clear();
                manager.opt.clusterAlgorithm .Add(ClusterAlgorithm.Kmeans);
                Run();
            }
        }

        private void thresholdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = CheckProfilesExist();
            ThresholdCForm km = new ThresholdCForm(manager.opt.threshold,flag,GetAlignedProfiles());

            DialogResult res = km.ShowDialog();
            if (res == DialogResult.OK)
            {
                manager.opt.threshold = km.localObj;
                manager.opt.clusterAlgorithm.Clear();
                manager.opt.clusterAlgorithm.Add(ClusterAlgorithm.BakerCluster);
                Run();
            }
        }
        private List<string> GetAlignedProfiles()
        {
            List<string> ff = new List<string>();
            foreach (var item in listBox1.Items)
                if (item.ToString().Contains("profiles"))
                    ff.Add(item.ToString().Replace(" profiles", ""));

            if (ff.Count == 0)
                ff = null;

            return ff;
        }
        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag = CheckProfilesExist();
            RankingCForm km;


            if (manager.opt.clusterAlgorithm != null && manager.opt.clusterAlgorithm.Count > 0)
                km = new RankingCForm(manager.opt.other,manager.opt.clusterAlgorithm[0],flag,GetAlignedProfiles());
            else
                km = new RankingCForm(manager.opt.other, ClusterAlgorithm.Jury1D, flag, GetAlignedProfiles());


            DialogResult res = km.ShowDialog();
            if (res == DialogResult.OK)
            {
                manager.opt.other = km.localObj;
                manager.opt.clusterAlgorithm.Clear();
                manager.opt.clusterAlgorithm.Add(km.alg);
                Run();
            }
        }

        private void endProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.RemoveJob((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value);

            if (manager.clOutput.ContainsKey((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value))
                manager.clOutput.Remove((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value);
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                    if (contextMenuStrip1.Items[i].Text.Contains("Save") || contextMenuStrip1.Items[i].Text.Contains("Show") ||
                        contextMenuStrip1.Items[i].Text.Contains("Close"))
                        contextMenuStrip1.Items[i].Enabled = false;
                    return;
            }
            else
                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                    contextMenuStrip1.Items[i].Enabled = true;

            if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[4].Value == null)
            {
                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                    if (contextMenuStrip1.Items[i].Text.Contains("Save current") || contextMenuStrip1.Items[i].Text.Contains("Show"))
                            contextMenuStrip1.Items[i].Enabled = false;
                gotoProcessToolStripMenuItem.Enabled = false;                
                finishProcessToolStripMenuItem.Text = "End Process";
            }
            else
            {
                finishProcessToolStripMenuItem.Text = "Close result";
                gotoProcessToolStripMenuItem.Enabled = true;
            }
            if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DefaultCellStyle.ForeColor == Color.Green)
            {
                if (manager.clOutput.ContainsKey((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value))
                {
                    List<string> options = ClusterGraphVis.GetVisOptions(manager.clOutput[(string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value]);
                    gotoProcessToolStripMenuItem.DropDownItems.Clear();
                    if (options != null)
                    {
                        gotoProcessToolStripMenuItem.Click -= gotoProcessToolStripMenuItem_Click;
                        foreach (var item in options)
                        {
                            ToolStripItem it = gotoProcessToolStripMenuItem.DropDownItems.Add(item);
                            it.Click += new EventHandler(SubMenuClick);

                        }
                    }
                    else
                    {
                        gotoProcessToolStripMenuItem.Click -= gotoProcessToolStripMenuItem_Click;
                        gotoProcessToolStripMenuItem.Click += gotoProcessToolStripMenuItem_Click;
                    }


                }
            }
        }
        private void SubMenuClick(object sender, EventArgs e)
        {
            ToolStripItem item = sender as ToolStripItem;
            try
            {
                DataGridViewCellCollection cell=dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells;
                string name = (string)cell[0].Value;
                    ClusterGraphVis vis = new ClusterGraphVis(manager.clOutput[name], name, manager.clOutput);
                    vis.Closing = CloseVisual;
                    vis.SClusters(name, cell[2].Value.ToString(), item.Text);
                   // cell[cell.Count - 1].Value = vis.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }
        private void finishProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.RemoveJob((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value);

            if (manager.clOutput.ContainsKey((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value))
                manager.clOutput.Remove((string)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[0].Value);
            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
        }

        private void clusterAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClustersAnalysis analysis = new ClustersAnalysis(manager.clOutput,true);
            analysis.analizeCluster = AnCluster;
            analysis.Show();
        }

        private void hushClusterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HashClusterForm hash = new HashClusterForm(manager.opt.hash,GetAlignedProfiles());

            DialogResult res=hash.ShowDialog();
            if (res == DialogResult.OK)
            {
                manager.opt.hash = hash.localInput;
                manager.opt.clusterAlgorithm.Clear();
                manager.opt.clusterAlgorithm.Add(ClusterAlgorithm.HashCluster);
                Run();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SetDataDirOptions();
            manager.opt.SaveDefault();
        }

        private void classificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClustersAnalysis analysis = new ClustersAnalysis(manager.clOutput);
            analysis.analizeCluster = ClassifyClusters;
            analysis.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {                
                DialogResult res;
                if (listBox1 != null && listBox1.Items.Count > 0)
                    folderBrowserDialog1.SelectedPath=(string)listBox1.Items[listBox1.Items.Count - 1];
                else
                    folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                 res= folderBrowserDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (!listBox1.Items.Contains(folderBrowserDialog1.SelectedPath))
                        listBox1.Items.Add(folderBrowserDialog1.SelectedPath);

                    SetDataDirOptions();
                }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    DCDForm dcd;
                    dcd = new DCDForm();


                    if (dcd.ShowDialog() == DialogResult.OK)
                    {
                        listBox1.Items.Add(dcd.dcd);
                        manager.opt.dcdFiles.Add(dcd.dcd);

                        SetDataDirOptions();
                    }
                }
                else
                    if (radioButton3.Checked)
                    {
                        openFileDialog1.Filter = "All files (*)|*";
                        DialogResult res = openFileDialog1.ShowDialog();

                        if (res == DialogResult.OK)
                        {
                            try
                            {
                                StreamReader stR = new StreamReader(openFileDialog1.FileName);
                                string line = stR.ReadLine();
                                while (line != null)
                                {
                                    if (Directory.Exists(line))
                                        listBox1.Items.Add(line);

                                    line = stR.ReadLine();
                                }
                                stR.Close();
                                SetDataDirOptions();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("File cannot be read. Error:" + ex.Message);
                            }
                        }
                    }
                    else
                        if (radioButton4.Checked)
                        {
                            DialogResult res;
                            if (listBox1.Items.Count > 0)
                        {
                                foreach (var item in listBox1.Items)
                                {
                                    if (!((string)item).Contains("profiles"))
                                    {
                                        res=MessageBox.Show("There are incompatible items on the list.\nAll items will be removed.\nDo you want to proceed?", 
                                            "Important question",
                                            MessageBoxButtons.YesNo);
                                        if (res == DialogResult.Yes)
                                        {
                                            listBox1.Items.Clear();                                            
                                            break;
                                        }
                                        else
                                            return;
                                    }
                                }
                            }
                            res = openFileDialog1.ShowDialog();
                            if (res == DialogResult.OK)
                            {
                                listBox1.Items.Add(openFileDialog1.FileName + " profiles");
                                radioButton1.Enabled = false;
                                radioButton2.Enabled = false;
                                radioButton3.Enabled = false;
                                SetDataDirOptions();
                            }
                        }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<object> lista = new List<object>();
            if (listBox1.SelectedItems.Count > 0)
                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                    lista.Add(listBox1.SelectedItems[i]) ;
            foreach(var item in lista)
                    listBox1.Items.Remove(item);

            bool test=false;
            foreach (var item in listBox1.Items)
            {
                if (((string)item).Contains("profiles"))
                {
                    test = true;
                    break;
                }
            }
            if (!test)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
            }

        }

        private void best5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ClusterOutput> oo = new List<ClusterOutput>();

            foreach (var item in manager.clOutput)            
                oo.Add(item.Value);
            
            Best5 bb = new Best5(oo);            
            bb.upList = UpBest5List;
            best5Res.Add(bb);
            bb.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            TextBoxView winError = new TextBoxView(new List<string>(ErrorBase.GetErrors()));

            winError.Show();

        }

        private void compareBest5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompareBest5Res winRes = new CompareBest5Res();
            winRes.StartCompare(best5Res);
            winRes.Show();
        }

        private void pairwiseComparisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PairComp pairWin = new PairComp(best5Res, false);
            pairWin.Show();
        }

        private void contextMenuStrip1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void best5RmsdCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ClusterOutput> oo = new List<ClusterOutput>();

            foreach (var item in manager.clOutput)
                oo.Add(item.Value);

            Best5 bb = new Best5(oo,true);
            bb.upList = UpBest5List;
            best5Res.Add(bb);
            bb.Show();
        }
        private void fractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ClusterOutput> oo = new List<ClusterOutput>();

            if (manager.clOutput == null)
            {
                MessageBox.Show("No data! Nothing to be done!");
                return;
            }

            foreach (var item in manager.clOutput)
                oo.Add(item.Value);

            FormFraction f = new FormFraction(oo[0].juryLike!=null);

            DialogResult res=f.ShowDialog();

            if (res == DialogResult.OK)
            {                
                Fraction fr = new Fraction(oo);
                ResultsForm frRes = new ResultsForm();
                Progress prog = new Progress(fr,frRes);
                prog.Start();
                prog.Show();
                Thread startProg = new Thread(fr.GetFraction);
                fractionParams p;
                p.clustersNum = f.consideredClusters;
                p.distance = f.dist;
                p.distThreshold = f.distThreshold;
                p.profileName = f.profileName;
                startProg.Start(p);         
               // fr.GetFraction(f.dist,f.profileName,f.distThreshold,f.consideredClusters);
                //prog.Close();
            }
        }

        private void loadExternalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Results (.cres)|*.cres|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                if (openFileDialog1.FileName.Length > 0)
                {
                    manager.LoadExternalF(openFileDialog1.FileName);
                    UpdateGridView();
                }
        }

        private void loadExternalPleiadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Results (.cres)|*.cres|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                if (openFileDialog1.FileName.Length > 0)
                {
                    manager.LoadExternalPleiades(openFileDialog1.FileName);
                    UpdateGridView();
                }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Results (.cres)|*.cres|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                if (openFileDialog1.FileName.Length > 0)
                {
                    manager.LoadExternalPconsD(openFileDialog1.FileName);
                    UpdateGridView();
                }

        }

        private void distancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> l = new List<string>();
            foreach (var item in listBox1.Items)
            {
                l.Add((string)item);
            }
            DistanceSave sa = new DistanceSave(l[0]);

            sa.ShowDialog();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult res;
          //  saveFileDialog1 = new OpenFileDialog();

            res=saveFileDialog1.ShowDialog();
            if(res==DialogResult.OK)
            {
                ClusterOutput sel = GetSelectedOutput();
                if (sel != null)
                {
                    if (sel.hNode == null)
                        sel.SaveTxt(saveFileDialog1.FileName);
                    else
                        MessageBox.Show("This save option is not available for hierarchical clustering");
                }
                else
                    MessageBox.Show("Something went wrong. Cannot save results!");

                
            }
        }
        private ClusterOutput GetSelectedOutput()
        {
              
            DataGridViewCellCollection cell=dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells;
                 string name = (string)cell[0].Value;
                 if(cell[cell.Count-1].Value!=null)
                    name = (string)cell[cell.Count-1].Value;

                 if (manager.clOutput.ContainsKey(name))
                     return manager.clOutput[name];

                 return null;
        }
        private void saveCurrentinTextModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem1_Click(sender, e);
        }
      
       
    }

}
