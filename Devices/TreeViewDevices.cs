using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devices
{
    public class TreeViewDevices
    {
        public TreeNode[] root = new TreeNode[0];

        private void AddRoot(TreeNode node)
        {
            if (root.Length == 0)
            {
                root = new TreeNode[1];
                root[0] = node;
            }
            else
            {
                TreeNode[] tmp = new TreeNode[root.Length + 1];
                for (int i = 0; i < root.Length; i++)
                {
                    tmp[i] = root[i];
                }
                tmp[root.Length] = node;
                root = tmp;
            }
        }

        public TreeViewDevices(Device[] devices)
        {
            if (devices != null)
            {
                foreach (var device in devices)
                {
                    if (root.Length == 0)
                    {
                        TreeNode tmp = new TreeNode(device.FamilyName);
                        AddRoot(tmp);
                        AddChild(root[0], device);
                    }
                    else
                    {
                        bool newRoot = true;
                        for (int i = 0; i < root.Length; i++)
                        {
                            if (root[i].Text == device.FamilyName)
                            {
                                AddChild(root[i], device);
                                newRoot = false;
                                break;
                            }
                        }
                        if (newRoot)
                        {
                            TreeNode tmp = new TreeNode(device.FamilyName);
                            AddRoot(tmp);
                            AddChild(root[root.Length - 1], device);
                        }
                    }
                }
            }
        }

        private void AddChild(TreeNode root, Device device)
        {
            bool newShortDesignation = true;

            for (int i = 0; i < root.Nodes.Count; i++)
            {
                if (root.Nodes[i].Text == device.modules[0].ShortDesignation)
                {
                    TreeNode tmp = new TreeNode(device.modules[0].OrderNumber);
                    root.Nodes[i].Nodes.Add(tmp);
                    newShortDesignation = false;
                    break;
                }
            }
            if (newShortDesignation)
            {
                TreeNode shortDesignation = new TreeNode();
                shortDesignation.Text = device.modules[0].ShortDesignation;
                TreeNode orderNumber = new TreeNode(device.modules[0].OrderNumber);
                shortDesignation.Nodes.Add(orderNumber);
                root.Nodes.Add(shortDesignation);
            }
        }
    }
}
