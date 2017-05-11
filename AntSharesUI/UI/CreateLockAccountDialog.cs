using AntShares.Core;
using AntShares.Cryptography.ECC;
using AntShares.VM;
using AntShares.Wallets;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AntShares.UI
{
    internal partial class CreateLockAccountDialog : Form
    {
        public CreateLockAccountDialog()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Program.CurrentWallet.GetContracts().Where(p => p.IsStandard).Select(p => Program.CurrentWallet.GetKey(p.PublicKeyHash).PublicKey).ToArray());
        }

        public Contract GetContract()
        {
            ECPoint publicKey = (ECPoint)comboBox1.SelectedItem;
            uint timestamp = dateTimePicker1.Value.ToTimestamp();
            using (ScriptBuilder sb = new ScriptBuilder())
            {
                sb.EmitPush(publicKey.EncodePoint(true));
                sb.EmitPush(timestamp);
                // Lock 1.0.0-preview2 in testnet tx:305fff937b3aedc003afdd2f2039ec7e089db4289bd6744dfa2817afcd471e31
                sb.EmitAppCall(UInt160.Parse("54030ae64f0a6d24bfda562778e0f4c9f1e24ecc").ToArray());
                return Contract.Create(publicKey.EncodePoint(true).ToScriptHash(), new[] { ContractParameterType.Signature }, sb.ToArray());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex >= 0;
        }
    }
}
