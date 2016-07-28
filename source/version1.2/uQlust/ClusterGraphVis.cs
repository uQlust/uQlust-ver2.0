using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Graph;
using uQlustCore;
using uQlustCore.Interface;
namespace Graph
{

    public class ClusterGraphVis : ClusterVis
    {
        string Name;
        Random r = new Random();
        int randomV;
        IVisual active = null;
        Dictionary<string,ClusterOutput> lOut;
        static List<string> hNodeOptions = new List<string>{"Dendrogram", "Circle Visual"};
        static List<string> clusterOptions = new List<string> { "Text List",  "Order Visual" };
        public ClusterGraphVis() { randomV = r.Next(); }
        public ClusterGraphVis(ClusterOutput output, string name, Dictionary<string, ClusterOutput> lOut = null) : base(output) { this.lOut = lOut; this.Name = name; randomV = r.Next(); }
        public ClosingForm Closing=null;

        public static List<string> GetVisOptions(ClusterOutput output)
        {
            if (output.hNode != null)
                return hNodeOptions;
            if (output.clusters != null)
                return clusterOptions;
               
            return null;
        }
        public override string ToString()
        {
            if (active != null)
                return Name + " " +randomV;       

            return "";
        }
        public void ActivateWindow()
        {
            if(active!=null)
                active.ToFront();           

        }
        public void CloseWindow()
        {
            if(active!=null)
                active.Close();          

        }
        public void SClusters(string item,string measureName,string option)
        {
            if (output.clusters != null)
            {
                switch(option)
                {
                    case "Order Visual":
                        if (active == null || !(active is VisOrder))
                        {
                            VisOrder visOrder;
                            visOrder = new VisOrder(output.clusters, item, null);
                            visOrder.closeForm = Closing;
                            active = visOrder;
                            visOrder.Show();
                        }
                        return;
                    case "Text List":
                        if (active == null || !(active is ListVisual))
                        {

                            ListVisual visBaker;
                            visBaker = new ListVisual(output.clusters, item);
                            visBaker.closeForm = Closing;
                            active = visBaker;
                            visBaker.Show();
                        }
                        return;
            }
             
            }
            if (output.hNode != null)
            {
               // win = new visHierar(output.hNode,item,measureName);
                if (option == null)
                    return;
                switch (option)
                {
                    case "Dendrogram":
                        if (active == null || !(active is visHierar))
                        {
                            visHierar winH;
                            winH = new visHierar(output.hNode, item, measureName);
                            winH.closeForm = Closing;
                            active = winH;
                            winH.Show();
                        }
                        return;                        
                    case "Circle Visual":
                        if (active == null || !(active is VisHierarCircle))
                        {

                            VisHierarCircle winC;
                            winC = new VisHierarCircle(output.hNode, item, measureName);
                            winC.closeForm = Closing;
                            active = winC;
                            winC.Show();
                        }
                        return;                        
                }


            }
            if (output.juryLike != null)
            {
                if (active == null || !(active is FormText))
                {

                    FormText showRes;
                    showRes = new FormText(output.juryLike, item);
                    showRes.closeForm = Closing;
                    active = showRes;
                    showRes.Show();
                }
                return;
            }

        }

    }
}
