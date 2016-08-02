using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using uQlustCore;
using uQlustCore.Interface;

namespace Graph
{
    interface SavePic
    {
        void SavePicture(string fileName,Bitmap bmp);
    }
    public partial class visHierar : Form,IVisual,SavePic
    {        
        HClusterNode hnode = null;
        HClusterNode rootNode = null;
        HClusterNode currentRootNode = null;
        public Dictionary< HClusterNode,Color> listNodes = null;
        Dictionary<string, Color> classColor = null;
        Dictionary<string, string> vecColor = null;
        Dictionary<Region, string> colorRegions = new Dictionary<Region, string>();
        List <Color> colorMap=new List<Color>();
        Stack<HClusterNode> nodesStack = new Stack<HClusterNode>();
        Bitmap buffer;
        int maxGraphicsY;// = 2000;
        bool linemode = false;
        bool markflag = false;
        bool viewType = false;
        double distanceStepY;
        double circleStep;
        int posStart = 40;
        int maxDist;
        HClusterNode maxHDist;
        HClusterNode minHDist;
        int currentHeight, currenWidth;
        int minRealDist;
        int maxRealDist;
        string measureName;
        public ClosingForm closeForm;
        bool clearAll = false;
        string winName;
        int mposX, mposY;
        public override string ToString()
        {
            return "Dendrogram";
        }
        public void ToFront()
        {
            this.BringToFront();
        }
        public visHierar(HClusterNode hnode,string name,string measureName,Dictionary<string,string> labels)
        {
            this.hnode = this.rootNode=hnode;                        
            InitializeComponent();
            buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            this.Text = name;
            winName = name;
            maxGraphicsY = buffer.Size.Height;// -2 * posStart;
            currentHeight = buffer.Height;
            currenWidth = buffer.Width;
            this.measureName = measureName;
            PrepareGraphNodes(buffer);
            int []tab=new int [4];

            tab[0] = 0; tab[1] = 85; tab[2] = 170; tab[3] = 255;

            for(int i=0;i<tab.Length;i++)
                for(int j=0;j<tab.Length;j++)
                    for (int n = 0; n < tab.Length; n++)
                        colorMap.Add(Color.FromArgb(tab[i],tab[j],tab[n]));

            vecColor=labels;
            SetColors();

        }
        public void SearchKmax(HClusterNode hNode)
        {
            Stack<HClusterNode> st = new Stack<HClusterNode>();
            HClusterNode current = null;
            maxDist = (int)hNode.levelDist;
            minRealDist = (int)hNode.levelDist;
            maxRealDist = (int)hNode.levelDist;
            st.Push(hNode);
            while (st.Count != 0)
            {
                current = st.Pop();
                if (current.levelDist > maxDist)
                    maxDist = (int)current.levelDist;
                if (current.levelDist > maxRealDist)
                    maxRealDist = (int)current.levelDist;

                if (current.levelDist < minRealDist)
                    minRealDist = (int)current.levelDist;

                if (current.joined != null)
                    foreach (var item in current.joined)
                        st.Push(item);
            }            
        }
        private void ClearBuffer()
        {
            Graphics g = Graphics.FromImage(buffer);
            g.Clear(pictureBox1.BackColor);
        }
        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
        
        }
        void DrawDistanceAx(HClusterNode localNode, Graphics gr,int lineThickness)
        {
            Pen p;
            p = new Pen(Color.Black);
            p.Width = lineThickness;
            int fontSize = (maxHDist.gNode.y - minHDist.gNode.y) / 90 + 5;
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", fontSize);

            gr.DrawLine(p, 5, maxHDist.gNode.y , 5, minHDist.gNode.y );
            gr.DrawLine(p, 5, minHDist.gNode.y , 10, minHDist.gNode.y);
            gr.DrawLine(p, 5, (minHDist.gNode.y) + (maxHDist.gNode.y) / 2, 10, minHDist.gNode.y  + maxHDist.gNode.y/ 2);
            
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();

            if(minHDist.realDist<1)
                gr.DrawString(minHDist.realDist.ToString("0.00"), drawFont, drawBrush, 10, minHDist.gNode.y -drawFont.SizeInPoints/2);
            else
                gr.DrawString(minHDist.realDist.ToString("0.0"), drawFont, drawBrush, 10, minHDist.gNode.y - drawFont.SizeInPoints / 2);
            double val;
            val = minHDist.realDist - maxHDist.realDist;
            if (minHDist.realDist < maxHDist.realDist)
                val = maxHDist.realDist - minHDist.realDist;
            if(val<1)
                gr.DrawString((val / 2).ToString("0.00"), drawFont, drawBrush, 10, minHDist.gNode.y + maxHDist.gNode.y/ 2 - drawFont.SizeInPoints / 2);
            else
                gr.DrawString((val / 2).ToString("0.0"), drawFont, drawBrush, 10, minHDist.gNode.y + maxHDist.gNode.y / 2 - drawFont.SizeInPoints / 2);
            gr.DrawString(measureName, drawFont, drawBrush, 5, 0);
        }
        void PrepareDrawGraph(HClusterNode startNode)
        {
            HClusterNode aux;
            Stack<HClusterNode> localStack = new Stack<HClusterNode>();
            
            localStack.Push(startNode);
            nodesStack.Push(startNode);
            while (localStack.Count > 0)
            {
                aux = localStack.Pop();                

                if (aux.joined!=null && aux.joined.Count > 0)
                {
                    for (int i = 0; i < aux.joined.Count; i++)
                    {
                        if (aux.joined[i].gNode.y < minHDist.gNode.y)
                            minHDist = aux.joined[i];
                        if (aux.joined[i].gNode.y > maxHDist.gNode.y)
                            maxHDist = aux.joined[i];
                        aux.joined[i].parent = aux;
                        localStack.Push(aux.joined[i]);
                        nodesStack.Push(aux.joined[i]);
                    }
                }                               
                
            }
        }
        void DrawStack( Graphics gr,int SizeY,int lineThick,bool legend,Color linesColor)
        {
            HClusterNode aux;
            while(nodesStack.Count>0)
            {
                aux = nodesStack.Pop();
                if(legend)
                    aux.gNode.DrawNode(gr, lineThick);
                if (nodesStack.Count == 0)
                    continue;

                Pen p;
                if(linesColor==Color.Empty)
                    p = new Pen(aux.color);
                else
                    p = new Pen(linesColor);
               // p = new Pen(Color.Black);
                p.Width = lineThick;
                if (aux.parent!=null)
                {
                    int x = aux.gNode.x;
                    gr.DrawLine(p, x, aux.gNode.y, x, aux.parent.gNode.y - lineThick / 2);
                    int y = aux.parent.gNode.y;
                    gr.DrawLine(p, aux.gNode.x, y, aux.parent.gNode.x, y);
                    //Console.WriteLine("x=" + x + " y=" + aux.gNode.y + " x=" + x + " y=" + (aux.parent.gNode.y - lineThick / 2));
                }
                if (aux.joined == null)
                {
                    if (vecColor != null)
                        if (vecColor.ContainsKey(aux.refStructure))
                        {
                            Pen r = new Pen(classColor[vecColor[aux.refStructure]]);
                            r.Width = lineThick;
                            gr.DrawLine(r, aux.gNode.x, aux.gNode.y, aux.gNode.x, SizeY);

                        }
                }


            }
        }
        private HClusterNode CheckClick(HClusterNode localNode, int mouseX, int mouseY)
        {
            HClusterNode clickNode = null;
            if (localNode.gNode.MouseClick(mouseX, mouseY))
                return localNode;
            if (localNode.joined == null)
                return null;
            for (int i = 0; i < localNode.joined.Count; i++)
            {
                clickNode = CheckClick(localNode.joined[i], mouseX, mouseY);
                if (clickNode != null)
                    return clickNode;
            }
            return null;

        }
        private void MakeGraphNodes(HClusterNode localNode)
        {
            Queue<HClusterNode> q = new Queue<HClusterNode>();

            q.Enqueue(localNode);
            while(q.Count!=0)
            {
                HClusterNode node=q.Dequeue();
                if (node.joined == null)
                    continue;
                List<int> rangeTab = new List<int>();
                int sum = 0;
                foreach (var item in node.joined)
                    sum += item.setStruct.Count;

                int diffArea = node.gNode.areaRight - node.gNode.areaLeft;
                for (int i = 0; i < node.joined.Count; i++)
                {
                    if (viewType)
                        rangeTab.Add(diffArea / node.joined.Count);
                    else
                        rangeTab.Add(diffArea * node.joined[i].setStruct.Count / sum);
                }
                for (int i = 0; i < node.joined.Count; i++)
                {
                    GraphNode graph;
                    node.joined[i].gNode = new GraphNode();
                    graph = node.joined[i].gNode;

                    //if (localNode.joined[i].levelDist >0)
                    graph.y = maxGraphicsY - (int)(distanceStepY * node.joined[i].levelDist) + posStart;
                    //else
                    //  graph.y = maxGraphicsY - 30;
                    if (i == 0)
                        graph.areaLeft = node.gNode.areaLeft;
                    else
                        graph.areaLeft = node.joined[i - 1].gNode.areaRight;

                    graph.areaRight = graph.areaLeft + rangeTab[i];

                    graph.x = (graph.areaRight + graph.areaLeft) / 2;

                    if (node.joined.Count > 0)
                        q.Enqueue(node.joined[i]);
                }                
            }
        }
        private void FillGraphNodes(HClusterNode localNode)
        {
            if (localNode.joined == null)
                return;
            List<int> rangeTab = new List<int>();
            int sum=0;
            foreach(var item in localNode.joined)
                sum+=item.setStruct.Count;

            int diffArea = localNode.gNode.areaRight - localNode.gNode.areaLeft;
            for(int i=0;i<localNode.joined.Count;i++)
            {
                if (viewType)
                    rangeTab.Add(diffArea / localNode.joined.Count);
                else
                    rangeTab.Add(diffArea * localNode.joined[i].setStruct.Count/sum);
            }
          //  int range = (localNode.gNode.areaRight - localNode.gNode.areaLeft) / localNode.joined.Count;
            for (int i = 0; i < localNode.joined.Count; i++)
            {
                GraphNode graph;
                localNode.joined[i].gNode = new GraphNode();
                graph = localNode.joined[i].gNode;

                //if (localNode.joined[i].levelDist >0)
                    graph.y = maxGraphicsY - (int)(distanceStepY * localNode.joined[i].levelDist) + posStart;
                //else
                  //  graph.y = maxGraphicsY - 30;
                    if (i == 0)                    
                        graph.areaLeft = localNode.gNode.areaLeft;                    
                    else
                        graph.areaLeft = localNode.joined[i - 1].gNode.areaRight;

                    graph.areaRight = graph.areaLeft + rangeTab[i];

                graph.x = (graph.areaRight + graph.areaLeft) / 2;

                if (localNode.joined.Count > 0)
                    FillGraphNodes(localNode.joined[i]);
            }

        }
        void PrepareGraphNodes(Bitmap bmp)
        {
            
            circleStep = 40.0 / rootNode.setStruct.Count;
            SearchKmax(rootNode);
            rootNode.gNode = new GraphNode();
            rootNode.gNode.areaLeft = 20;
            rootNode.gNode.areaRight = bmp.Size.Width - 20;
            rootNode.gNode.x = (rootNode.gNode.areaLeft+ rootNode.gNode.areaRight) / 2;            
           
            //rootNode.gNode.x = this.Size.Width / 2;



            maxGraphicsY = bmp.Size.Height-posStart-30;// -3 * posStart;
            if (maxDist == 0)
                throw new Exception("Dendrgrom cannot be build. Wrong distances!");
            distanceStepY = ((float)maxGraphicsY) /maxDist;
            rootNode.gNode.y = maxGraphicsY - (int)(distanceStepY * rootNode.levelDist) + posStart;
            //Console.WriteLine("node=" + rootNode.gNode.y + " ldist=" + rootNode.levelDist + " step=" + distanceStepY + " aa=" + distanceStepY * rootNode.levelDist);
            //maxX = hnode.gNode.areaRight;

           // FillGraphNodes(rootNode);
            MakeGraphNodes(rootNode);
            RecalcPositions(rootNode);
        }
        void RecalcPositions(HClusterNode node)
        {
            if(node.joined!=null && node.joined.Count!=0)
            {
                foreach (var item in node.joined)
                    RecalcPositions(item);

                node.gNode.x = 0;
                foreach(var item in node.joined)
                {
                    node.gNode.x += item.gNode.x;
                }
                node.gNode.x /= node.joined.Count;
            }
            

        }
        string CheckColorRegion(int mouseX, int mouseY)
        {
            foreach (var item in colorRegions)
            {
                if (item.Key.IsVisible(mouseX, mouseY))
                    return item.Value;
            }

            return null;
        }
        private void Form2_ResizeEnd(object sender, EventArgs e)
        {
            clearAll = true;
            buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            PrepareGraphNodes(buffer);
            DrawOnBuffer(buffer,true,1,Color.Empty);
            this.Invalidate();
            maxGraphicsY = pictureBox1.Height-posStart-30;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClusterOutput output = new ClusterOutput();
            output.hNode = hnode;
            ClusterVis wrCluster=new ClusterVis(output);
            saveFileDialog1.DefaultExt = "png";
            saveFileDialog1.Filter = "Png files|*.png";
            DialogResult res=saveFileDialog1.ShowDialog();
            if (res == DialogResult.OK && saveFileDialog1.FileName.Length > 0)
            {
                Resolution resForm = new Resolution(buffer.Width, buffer.Height,Color.Black);
                res=resForm.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Bitmap bmp = new Bitmap(resForm.WidthR, resForm.HeightR);
                    PrepareGraphNodes(bmp);

                    DrawOnBuffer(bmp,resForm.ShowLegend,resForm.LineThickness,resForm.LinesColor);
                    SavePicture(saveFileDialog1.FileName, bmp);
                    PrepareGraphNodes(buffer);
                }
                //       this.SavePicture(saveFileDialog1.FileName);          
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (hnode != rootNode)
            {
                rootNode = hnode;
                PrepareGraphNodes(buffer);
                ClearBuffer();
                pictureBox1.Invalidate();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeForm != null)
                closeForm(winName);
        }
        public void SavePicture(string fileName,Bitmap bmp)
        {
            if (!fileName.Contains(".png"))
                fileName += ".png";
            bmp.Save(fileName, ImageFormat.Png);
        }
        private void DrawClassColor(Graphics g,Size sizeBuff)
        {
            if (classColor != null)
            {
                colorRegions.Clear();
                SolidBrush drawBrush;
                Font drawFont = new System.Drawing.Font("Arial", 8);
                g.PageUnit = GraphicsUnit.Pixel;
                int y = 20;
                int x=0;
                foreach (var item in classColor)
                {
                    SizeF textSize = g.MeasureString(item.Key, drawFont);
                    if (x < textSize.Width)
                        x = (int)textSize.Width;
                }
                x = sizeBuff.Width - x - 20;                
                    foreach (var item in classColor)
                    {

                        drawBrush = new System.Drawing.SolidBrush(item.Value);
                        g.FillRectangle(drawBrush, x, y, 15, 10);
                        drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                        g.DrawString(item.Key, drawFont, drawBrush, x + 20, y);

                        Rectangle rect = new Rectangle(x, y, 15, 10);
                        colorRegions.Add(new Region(rect), item.Key);

                        y += 25;
                        if (y > sizeBuff.Height)
                        {
                            x += 150;
                            y = 25;
                        }

                    }
            }
        }
        private void DrawOnBuffer(Bitmap bmp,bool legend,int lineThickness,Color linesColor)
        {
            Graphics g;
            g = Graphics.FromImage(bmp);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            maxHDist = minHDist = rootNode;
            PrepareDrawGraph(rootNode);
            DrawStack(g, bmp.Size.Height,lineThickness,legend,linesColor);
            //DrawGraph(rootNode, g,bmp.Size.Height);
            DrawDistanceAx(rootNode, g,lineThickness);
            if(legend)
                DrawClassColor(g,bmp.Size);            
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
         
           if (currentHeight == pictureBox1.Height && currenWidth == pictureBox1.Width && buffer!=null && currentRootNode==rootNode)
            {
                e.Graphics.Clear(this.BackColor);
                e.Graphics.DrawImage(buffer, 0, 0);
                if (linemode)
                {

                    Pen p = new Pen(Color.Brown);
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                    System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();


                    if (listNodes != null)
                        e.Graphics.DrawString(listNodes.Count.ToString(), drawFont, drawBrush, 50, mposY);
                    e.Graphics.DrawLine(p, 0, mposY, buffer.Width, mposY);
                    return;
                }
                if (listNodes != null)
                {
                    SolidBrush brush;
                    foreach (var item in listNodes)
                    {
                        brush = new SolidBrush(item.Value);
                        if (rootNode.IsVisible(item.Key))
                            e.Graphics.FillEllipse(brush, item.Key.gNode.x, item.Key.gNode.y - 7, 7, 7);
                    }
                }

            }
            else
            {
              
                if (hnode != null)
                {
                    if (clearAll)
                    {
                        e.Graphics.Clear(this.BackColor);
                        clearAll = false;
                    }
/*                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;*/
                    if (buffer == null || buffer.Width != pictureBox1.Width || buffer.Height != pictureBox1.Height)
                    {
                        buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    }
                    currenWidth = buffer.Width;
                    currentHeight = buffer.Height;
                    currentRootNode = rootNode;

                    DrawOnBuffer(buffer,true,1,Color.Empty);
                    e.Graphics.DrawImage(buffer, 0, 0);
                   // buffer.Save("proba.png", ImageFormat.Png);
                    //maxHDist = minHDist = rootNode;
                    //DrawGraph(rootNode, e.Graphics);
                    //DrawDistanceAx(rootNode, e.Graphics);

                    if (listNodes != null)
                    {
                        SolidBrush brush;
                        foreach (var item in listNodes)
                        {
                            brush= new SolidBrush(item.Value);
                            if (rootNode.IsVisible(item.Key))
                                e.Graphics.FillEllipse(brush, item.Key.gNode.x, item.Key.gNode.y - 7, 7, 7);
                        }
                    }

                }
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            HClusterNode clickNode;

            if (linemode)
            {
                linemode = false;
                if (listNodes.Count > 0)
                {
                    SaveMarkedClusters.Enabled = true;
                    orderVis.Enabled = true;
                }
                pictureBox1.Invalidate();
                return;
            }
            if (markflag)
            {
                clickNode = CheckClick(rootNode, e.X, e.Y);
                if (clickNode != null)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        if (listNodes.ContainsKey(clickNode))
                            listNodes.Remove(clickNode);
                    }
                    else
                    {
                        if (listNodes == null)
                            listNodes = new Dictionary<HClusterNode, Color>();
                        if (!listNodes.ContainsKey(clickNode))
                            listNodes.Add(clickNode,Color.Red);
                    }
                    if (listNodes.Count > 0)
                    {
                        SaveMarkedClusters.Enabled = true;
                        orderVis.Enabled = true;
                    }
                    pictureBox1.Invalidate();
                }


                return;
            }
            clickNode = CheckClick(rootNode, e.X, e.Y);

            if (clickNode != null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    rootNode = clickNode;
                    PrepareGraphNodes(buffer);
                    ClearBuffer();
                    pictureBox1.Invalidate();

                    //this.Invalidate();
                }
                else
                {
                    FormText info = new FormText(clickNode);
                    info.Show();
                }
            }
            string lab = CheckColorRegion(e.X, e.Y);
            if (lab != null)
            {
                DialogResult res;
                colorDialog1 = new ColorDialog();
                int []colorTab=new int [classColor.Count];
                int i=0;
                foreach (var item in classColor)
                    colorTab[i++] = ColorTranslator.ToOle(item.Value);
                colorDialog1.CustomColors = colorTab;
   
                colorDialog1.Color = classColor[lab];
                
                res = colorDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    classColor[lab] = Color.FromArgb(colorDialog1.Color.R, colorDialog1.Color.G, colorDialog1.Color.B);
                    buffer = null;
                    pictureBox1.Invalidate();
                    pictureBox1.Refresh();
                }
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (!linemode)
            {
                linemode = true;
                /*buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);

                Graphics g = Graphics.FromImage(buffer); ;

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                DrawGraph(rootNode, g);
                DrawDistanceAx(rootNode, g);*/
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (linemode)
            {
                mposX = mposY;
                mposY = e.Y;
                listNodes=rootNode.CutDendrog(-(int)((e.Y-maxGraphicsY - posStart)/distanceStepY));
                pictureBox1.Invalidate();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            markflag = !markflag;
            
            if(markflag)
                toolStripButton4.Image = ((System.Drawing.Image)uQlustCore.Properties.Resources.Flag2);
            else
                toolStripButton4.Image = ((System.Drawing.Image)uQlustCore.Properties.Resources.Flag);
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                markflag = false ;
                linemode = false;
                if(listNodes!=null)
                    listNodes.Clear();
                SaveMarkedClusters.Enabled = false;
                pictureBox1.Invalidate();
            }
            
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (listNodes != null)
            {
                listNodes.Clear();
                SaveMarkedClusters.Enabled = false;
                pictureBox1.Invalidate();
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files|*.txt";
            saveFileDialog1.DefaultExt = "txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Length > 0)
                {
                    string name = saveFileDialog1.FileName;
                    if (!name.Contains(".txt"))
                        name += ".txt";
                    StreamWriter file = new StreamWriter(name);

                    int i = 0;
                    foreach (var item in listNodes)
                    {
                        item.Key.SaveNode(file, i++);
                    }

                    file.Close();
                }
            }

        }
        public void ShowCloseButton()
        {
            toolStripButton5.Visible = true;
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();  
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

            List<string> structures = new List<string>();

            foreach(var item in rootNode.setStruct)
            {
                structures.Add(rootNode.dirName + Path.DirectorySeparatorChar + item);
            }

            SelectBestToNatiive select = new SelectBestToNatiive(structures);
            select.ShowDialog();
            if(select.DialogResult==DialogResult.OK)
            {

                if (select.bestStructures != null && select.bestStructures.Count > 0)
                    listNodes=rootNode.MarkNodes(select.bestStructures,Color.Red);
                if(select.bestJuryStructures!=null && select.bestJuryStructures.Count>0)
                {
                    Dictionary <HClusterNode,Color> aux=new Dictionary <HClusterNode,Color>();

                    aux = rootNode.MarkNodes(select.bestJuryStructures, Color.Blue);

                    if (listNodes == null)
                        listNodes = aux;
                    else
                        foreach (var item in aux)
                            if (!listNodes.ContainsKey(item.Key))
                                listNodes.Add(item.Key,item.Value);
                }
                
                pictureBox1.Invalidate();
            }
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DialogResult res;
            SelectStruct s = new SelectStruct(hnode.setStruct);

            res=s.ShowDialog();
            if(res==DialogResult.OK)
            {
                hnode.ColorNode(s.selectedStruct, 2);
                buffer = null;
                pictureBox1.Invalidate();
                pictureBox1.Refresh(); 
            }
        }

        private void orderVis_Click(object sender, EventArgs e)
        {
            List<List<string>> clusterList=new List<List<string>>();
            List<int[]> colorMap = new List<int[]>();
            int[] tab = new int[4];
            tab[0] = 0; tab[1] = 65; tab[2] = 150; tab[3] = 235;

            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab.Length; j++)
                    for (int n = 0; n < tab.Length; n++)
                    {
                        int[] aux = new int[3];
                        aux[0] = tab[i];
                        aux[1] = tab[j];
                        aux[2] = tab[n];

                        colorMap.Add(aux);
                    }
            int counter=0;
            List<Color> clusterColor = new List<Color>();
            List<HClusterNode> hk=new List<HClusterNode>(listNodes.Keys);
             VisOrder visFrame =null;
             if (hk.Count < colorMap.Count)
             {
                 for (int i = 0; i < hk.Count; i++)
                 {
                     clusterList.Add(hk[i].setStruct);
                     if (listNodes.Count < colorMap.Count)
                     {
                         Color r = Color.FromArgb(colorMap[counter][0], colorMap[counter][1], colorMap[counter][2]);
                         listNodes[hk[i]] = r;
                         clusterColor.Add(r);
                     }
                     counter += colorMap.Count / (hk.Count+10);
                 }
                 visFrame = new VisOrder(clusterList, null, clusterColor);
             }
             else
             {
                 for (int i = 0; i < hk.Count; i++)
                     clusterList.Add(hk[i].setStruct);
                 visFrame = new VisOrder(clusterList, null, null);
             }
            visFrame.Show();
            this.Invalidate();
            this.Refresh();
        }
        private void SetColors()
        {            
                if (vecColor == null)
                    return;
                classColor = new Dictionary<string, Color>();
                foreach (var item in vecColor)
                {
                    if (!classColor.ContainsKey(item.Value))
                        classColor.Add(item.Value, Color.Azure);
                }

                double step = (colorMap.Count - 2) / classColor.Keys.Count;

                List<string> ll = new List<string>(classColor.Keys);

                int count = 2;
                for (int i = 0; i < ll.Count; i++)
                {
                    classColor[ll[i]] = colorMap[count];
                    count = (int)((i + 1) * step);
                }
                buffer = null;
                pictureBox1.Invalidate();
                pictureBox1.Refresh();            

            
        }
        private void ColorAndLabels(string fileName)
        {
                
                vecColor = ClusterOutput.ReadLabelsFile(fileName);
                SetColors();
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DialogResult res= openFileDialog1.ShowDialog();

            if(res==DialogResult.OK)
            {
                ColorAndLabels(openFileDialog1.FileName);
            }

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            viewType = !viewType;
            PrepareGraphNodes(buffer);
            //ClearBuffer();
            buffer = null;
            pictureBox1.Invalidate();
           
        }



    }
}
