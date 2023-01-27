using SC2;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public enum WMessages : int
{
    WM_CHAR = 0x102     //char
}

namespace SC2Macro
{
    public partial class MainForm : Form
    {
        [DllImport("user32.dll")]
        public static extern int PostMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string SClassName, string SWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hWnd1, IntPtr hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr findname);
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr findname, int howShow);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public MainForm()
        {
            InitializeComponent();
        }

        public IntPtr process;
        public static String iniPath = Application.StartupPath + @"\S2RD.ini";
        private void MainForm_Load(object sender, EventArgs e)
        {
            process = FindWindow(null, "스타크래프트 II");
            if (process.Equals(IntPtr.Zero))
            {
                MessageBox.Show("스타크래프트를 찾을 수 없습니다.");
            }
            else
            {
                LoadSetting();
            }
        }
        
        public void SendChar(char key)
        {
            PostMessage(process, (int)WMessages.WM_CHAR, key, 0);
        }
        public void SendCommand(String str)
        {
            PostMessage(process, 0x100, 0x0D, 0);
            foreach (char i in str)
            {
                SendChar(i);
            }
            PostMessage(process, 0x100, 0x0D, 0);
        }

        #region 모든 시민
        String allCivil = "";
        private void btnAllCivil1_Click(object sender, EventArgs e)
        {
            allCivil = "lowUnit";
            allCivilBackColorReset();
            btnAllCivil1.BackColor = Color.DeepSkyBlue;
            SendCommand("@d");
            IniFile.SetValue(iniPath, "설정", "AllCivil", "LowUnit");
        }

        private void btnAllCivil2_Click(object sender, EventArgs e)
        {
            allCivil = "unit";
            allCivilBackColorReset();
            btnAllCivil2.BackColor = Color.DeepSkyBlue;
            SendCommand("@u");
            IniFile.SetValue(iniPath, "설정", "AllCivil", "Unit");
        }

        private void btnAllCivil3_Click(object sender, EventArgs e)
        {
            allCivil = "mineral";
            allCivilBackColorReset();
            btnAllCivil3.BackColor = Color.DeepSkyBlue;
            SendCommand("@l");
            IniFile.SetValue(iniPath, "설정", "AllCivil", "Mineral");
        }

        private void btnAllCivil4_Click(object sender, EventArgs e)
        {
            allCivil = "gas";
            allCivilBackColorReset();
            btnAllCivil4.BackColor = Color.DeepSkyBlue;
            SendCommand("@r");
            IniFile.SetValue(iniPath, "설정", "AllCivil", "Gas");
        }

        private void allCivilBackColorReset()
        {
            btnAllCivil1.BackColor = Color.WhiteSmoke;
            btnAllCivil2.BackColor = Color.WhiteSmoke;
            btnAllCivil3.BackColor = Color.WhiteSmoke;
            btnAllCivil4.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 뽑기 시민
        String unitCivil = "";
        private void btnUnitCivil1_Click(object sender, EventArgs e)
        {
            unitCivil = "lowUnit";
            unitCivilBackColorReset();
            btnUnitCivil1.BackColor = Color.DeepSkyBlue;
            SendCommand("@bd");
            IniFile.SetValue(iniPath, "설정", "UnitCivil", "LowUnit");
        }

        private void btnUnitCivil2_Click(object sender, EventArgs e)
        {
            unitCivil = "unit";
            unitCivilBackColorReset();
            btnUnitCivil2.BackColor = Color.DeepSkyBlue;
            SendCommand("@bu");
            IniFile.SetValue(iniPath, "설정", "UnitCivil", "Unit");
        }
        private void unitCivilBackColorReset()
        {
            btnUnitCivil1.BackColor = Color.WhiteSmoke;
            btnUnitCivil2.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 강화 시민
        String foreCivil = "";
        private void btnForeCivil1_Click(object sender, EventArgs e)
        {
            foreCivil = "lowUnit";
            forceCivilBackColorReset();
            btnForeCivil1.BackColor = Color.DeepSkyBlue;
            SendCommand("@rd");
            IniFile.SetValue(iniPath, "설정", "ForceCivil", "LowUnit");
        }

        private void btnForeCivil2_Click(object sender, EventArgs e)
        {
            foreCivil = "unit";
            forceCivilBackColorReset();
            btnForeCivil2.BackColor = Color.DeepSkyBlue;
            SendCommand("@ru");
            IniFile.SetValue(iniPath, "설정", "ForceCivil", "Unit");
        }

        private void btnForeCivil3_Click(object sender, EventArgs e)
        {
            foreCivil = "mineral";
            forceCivilBackColorReset();
            btnForeCivil3.BackColor = Color.DeepSkyBlue;
            SendCommand("@rl");
            IniFile.SetValue(iniPath, "설정", "ForceCivil", "Mineral");
        }

        private void btnForeCivil4_Click(object sender, EventArgs e)
        {
            foreCivil = "gas";
            forceCivilBackColorReset();
            btnForeCivil4.BackColor = Color.DeepSkyBlue;
            SendCommand("@rr");
            IniFile.SetValue(iniPath, "설정", "ForceCivil", "Gas");
        }
        private void forceCivilBackColorReset()
        {
            btnForeCivil1.BackColor = Color.WhiteSmoke;
            btnForeCivil2.BackColor = Color.WhiteSmoke;
            btnForeCivil3.BackColor = Color.WhiteSmoke;
            btnForeCivil4.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 자원 시민
        String resourceCivil = "";
        private void btnResourceCivil1_Click(object sender, EventArgs e)
        {
            resourceCivil = "mineral";
            resourceCivilBackColorReset();
            btnResourceCivil1.BackColor = Color.DeepSkyBlue;
            SendCommand("@gl");
            IniFile.SetValue(iniPath, "설정", "ResourceCivil", "Mineral");
        }

        private void btnResourceCivil2_Click(object sender, EventArgs e)
        {
            resourceCivil = "gas";
            resourceCivilBackColorReset();
            btnResourceCivil1.BackColor = Color.DeepSkyBlue;
            SendCommand("@gr");
            IniFile.SetValue(iniPath, "설정", "ResourceCivil", "Gas");
        }
        private void resourceCivilBackColorReset()
        {
            btnResourceCivil1.BackColor = Color.WhiteSmoke;
            btnResourceCivil2.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 라이프 경고
        bool lifeAlarm = false;
        private void btnLifeAlarmOn_Click(object sender, EventArgs e)
        {
            lifeAlarm = true;
            lifeAlarmBackColorReset();
            btnLifeAlarmOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@pd on");
            IniFile.SetValue(iniPath, "설정", "LifeAlarm", "On");
        }
        private void btnLifeAlarmOff_Click(object sender, EventArgs e)
        {
            lifeAlarm = false;
            lifeAlarmBackColorReset();
            btnLifeAlarmOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@pd off");
            IniFile.SetValue(iniPath, "설정", "LifeAlarm", "Off");
        }
        private void lifeAlarmBackColorReset()
        {
            btnLifeAlarmOn.BackColor = Color.WhiteSmoke;
            btnLifeAlarmOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 보관함 정렬
        bool unitSort = false;
        private void btnUnitSortOn_Click(object sender, EventArgs e)
        {
            unitSort = true;
            unitSortBackColorReset();
            btnUnitSortOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@a on");
            IniFile.SetValue(iniPath, "설정", "UnitSort", "On");
        }

        private void btnUnitSortOff_Click(object sender, EventArgs e)
        {
            unitSort = false;
            unitSortBackColorReset();
            btnUnitSortOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@a off");
            IniFile.SetValue(iniPath, "설정", "UnitSort", "Off");
        }
        private void unitSortBackColorReset()
        {
            btnUnitSortOn.BackColor = Color.WhiteSmoke;
            btnUnitSortOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 유닛 저장
        bool unitSave = false;
        private void btnUnitSaveOn_Click(object sender, EventArgs e)
        {
            unitSave = true;
            unitSaveBackColorReset();
            btnUnitSaveOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@us on");
            IniFile.SetValue(iniPath, "설정", "UnitSave", "On");
        }
        private void btnUnitSaveOff_Click(object sender, EventArgs e)
        {
            unitSave = false;
            unitSaveBackColorReset();
            btnUnitSaveOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@us off");
            IniFile.SetValue(iniPath, "설정", "UnitSave", "Off");
        }
        private void unitSaveBackColorReset()
        {
            btnUnitSaveOn.BackColor = Color.WhiteSmoke;
            btnUnitSaveOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 시민 저장
        bool civilSave = false;
        private void btnCivilSaveOn_Click(object sender, EventArgs e)
        {
            civilSave = true;
            civilSaveBackColorReset();
            btnCivilSaveOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@cs on");
            IniFile.SetValue(iniPath, "설정", "SaveCivil", "On");
        }

        private void btnCivilSaveOff_Click(object sender, EventArgs e)
        {
            civilSave = false;
            civilSaveBackColorReset();
            btnCivilSaveOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@cs off");
            IniFile.SetValue(iniPath, "설정", "SaveCivil", "Off");
        }
        private void civilSaveBackColorReset()
        {
            btnCivilSaveOn.BackColor = Color.WhiteSmoke;
            btnCivilSaveOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 자동 판매
        bool autoSale = false;
        private void btnAutoSaleOn_Click(object sender, EventArgs e)
        {
            autoSale = true;
            autoSaleBackColorReset();
            btnAutoSaleOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@as on");
            IniFile.SetValue(iniPath, "설정", "AutoSale", "On");
        }
        private void btnAutoSaleOff_Click(object sender, EventArgs e)
        {
            autoSale = false;
            autoSaleBackColorReset();
            btnAutoSaleOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@as off");
            IniFile.SetValue(iniPath, "설정", "AutoSale", "Off");
        }
        private void autoSaleBackColorReset()
        {
            btnAutoSaleOn.BackColor = Color.WhiteSmoke;
            btnAutoSaleOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 고급 유닛 판매
        bool unitSale1 = false;
        private void btnUnitSale1On_Click(object sender, EventArgs e)
        {
            unitSale1 = true;
            unitSale1BackColorReset();
            btnUnitSale1On.BackColor = Color.DeepSkyBlue;
            SendCommand("@s1 on");
            IniFile.SetValue(iniPath, "설정", "UnitSale1", "On");
        }
        private void btnUnitSale1Off_Click(object sender, EventArgs e)
        {
            unitSale1 = false;
            unitSale1BackColorReset();
            btnUnitSale1Off.BackColor = Color.DeepSkyBlue;
            SendCommand("@s1 off");
            IniFile.SetValue(iniPath, "설정", "UnitSale1", "Off");
        }
        private void unitSale1BackColorReset()
        {
            btnUnitSale1On.BackColor = Color.WhiteSmoke;
            btnUnitSale1Off.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 최고급 유닛 판매
        bool unitSale2 = false;
        private void btnUnitSale2On_Click(object sender, EventArgs e)
        {
            unitSale2 = true;
            unitSale2BackColorReset();
            btnUnitSale2On.BackColor = Color.DeepSkyBlue;
            SendCommand("@s2 on");
            IniFile.SetValue(iniPath, "설정", "UnitSale2", "On");
        }
        private void btnUnitSale2Off_Click(object sender, EventArgs e)
        {
            unitSale2 = false;
            unitSale2BackColorReset();
            btnUnitSale2Off.BackColor = Color.DeepSkyBlue;
            SendCommand("@s2 off");
            IniFile.SetValue(iniPath, "설정", "UnitSale2", "Off");
        }
        private void unitSale2BackColorReset()
        {
            btnUnitSale2On.BackColor = Color.WhiteSmoke;
            btnUnitSale2Off.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 희귀 판매
        bool unitSale3 = false;
        private void btnUnitSale3On_Click(object sender, EventArgs e)
        {
            unitSale3 = true;
            unitSale3BackColorReset();
            btnUnitSale3On.BackColor = Color.DeepSkyBlue;
            SendCommand("@s3 on");
            IniFile.SetValue(iniPath, "설정", "UnitSale3", "On");
        }
        private void btnUnitSale3Off_Click(object sender, EventArgs e)
        {
            unitSale3 = false;
            unitSale3BackColorReset();
            btnUnitSale3Off.BackColor = Color.DeepSkyBlue;
            SendCommand("@s3 off");
            IniFile.SetValue(iniPath, "설정", "UnitSale3", "Off");
        }
        private void unitSale3BackColorReset()
        {
            btnUnitSale3On.BackColor = Color.WhiteSmoke;
            btnUnitSale3Off.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 전설 판매
        bool unitSale4;
        private void btnUnitSale4On_Click(object sender, EventArgs e)
        {
            unitSale4 = true;
            unitSale4BackColorReset();
            btnUnitSale4On.BackColor = Color.DeepSkyBlue;
            SendCommand("@s4 on");
            IniFile.SetValue(iniPath, "설정", "UnitSale4", "On");
        }
        private void btnUnitSale4Off_Click(object sender, EventArgs e)
        {
            unitSale4 = false;
            unitSale4BackColorReset();
            btnUnitSale4Off.BackColor = Color.DeepSkyBlue;
            SendCommand("@s4 off");
            IniFile.SetValue(iniPath, "설정", "UnitSale4", "Off");
        }
        private void unitSale4BackColorReset()
        {
            btnUnitSale4On.BackColor = Color.WhiteSmoke;
            btnUnitSale4Off.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 타워 판매
        bool towerSale = false;
        private void btnTowerSaleOn_Click(object sender, EventArgs e)
        {
            towerSale = true;
            towerSaleBackColorReset();
            btnTowerSaleOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@st on");
            IniFile.SetValue(iniPath, "설정", "TowerSale", "On");
        }
        private void btnTowerSaleOff_Click(object sender, EventArgs e)
        {
            towerSale = false;
            towerSaleBackColorReset();
            btnTowerSaleOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@st off");
            IniFile.SetValue(iniPath, "설정", "TowerSale", "Off");
        }
        private void towerSaleBackColorReset()
        {
            btnTowerSaleOn.BackColor = Color.WhiteSmoke;
            btnTowerSaleOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        #region 유물 판매
        bool relicSale = false;
        private void btnRelicSaleOn_Click(object sender, EventArgs e)
        {
            relicSale = true;
            relicSaleBackColorReset();
            btnRelicSaleOn.BackColor = Color.DeepSkyBlue;
            SendCommand("@sr on");
            IniFile.SetValue(iniPath, "설정", "RelicSale", "On");
        }
        private void btnRelicSaleOff_Click(object sender, EventArgs e)
        {
            relicSale = false;
            relicSaleBackColorReset();
            btnRelicSaleOff.BackColor = Color.DeepSkyBlue;
            SendCommand("@sr off");
            IniFile.SetValue(iniPath, "설정", "RelicSale", "Off");
        }
        private void relicSaleBackColorReset()
        {
            btnRelicSaleOn.BackColor = Color.WhiteSmoke;
            btnRelicSaleOff.BackColor = Color.WhiteSmoke;
        }
        #endregion

        private void btnCancleCommand_Click(object sender, EventArgs e)
        {
            SendCommand("@c");
        }

        private void btnAllCommandApply_Click(object sender, EventArgs e)
        {
            if (allCivil.Equals("lowUnit")) SendCommand("@d");
            else if (allCivil.Equals("unit")) SendCommand("@u");
            else if (allCivil.Equals("mineral")) SendCommand("@l");
            else if (allCivil.Equals("gas")) SendCommand("@r");

            if (unitCivil.Equals("lowUnit")) SendCommand("@bd");
            else if (unitCivil.Equals("unit")) SendCommand("@bu");

            if (foreCivil.Equals("lowUnit")) SendCommand("@rd");
            else if (foreCivil.Equals("unit")) SendCommand("@ru");
            else if (foreCivil.Equals("mineral")) SendCommand("@rl");
            else if (foreCivil.Equals("gas")) SendCommand("@rr");

            if (resourceCivil.Equals("mineral")) SendCommand("@gl");
            else if (resourceCivil.Equals("gas")) SendCommand("@gr");

            if (lifeAlarm) SendCommand("@pd on");
            else SendCommand("@pd off");

            if (unitSort) SendCommand("@a on");
            else SendCommand("@a off");

            if (unitSave) SendCommand("@us on");
            else SendCommand("@us off");
            
            if (civilSave) SendCommand("@cs on");
            else SendCommand("@cs off");

            if (autoSale) SendCommand("@as on");
            else SendCommand("@as off");

            if (unitSale1) SendCommand("@s1 on");
            else SendCommand("@s1 off");

            if (unitSale2) SendCommand("@s2 on");
            else SendCommand("@s2 off");

            if (unitSale3) SendCommand("@s3 on");
            else SendCommand("@s3 off");

            if (unitSale4) SendCommand("@s4 on");
            else SendCommand("@s4 off");

            if (towerSale) SendCommand("@st on");
            else SendCommand("@st off");

            if (relicSale) SendCommand("@sr on");
            else SendCommand("@sr off");
        }

        private void LoadSetting()
        {
            String loadAllCivil = IniFile.GetValue(MainForm.iniPath, "설정", "AllCivil", "");
            if (loadAllCivil.Equals("LowUnit"))
            {
                allCivil = "lowUnit";
                btnAllCivil1.BackColor = Color.DeepSkyBlue;
            }
            if (loadAllCivil.Equals("Unit"))
            {
                allCivil = "unit";
                btnAllCivil2.BackColor = Color.DeepSkyBlue;
            }
            if (loadAllCivil.Equals("Mineral"))
            {
                allCivil = "mineral";
                btnAllCivil3.BackColor = Color.DeepSkyBlue;
            }
            if (loadAllCivil.Equals("Gas"))
            {
                allCivil = "gas";
                btnAllCivil4.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitCivil = IniFile.GetValue(MainForm.iniPath, "설정", "UnitCivil", "");
            if (loadUnitCivil.Equals("LowUnit"))
            {
                unitCivil = "lowUnit";
                btnUnitCivil1.BackColor = Color.DeepSkyBlue;
            }
            if (loadUnitCivil.Equals("Unit"))
            {
                unitCivil = "unit";
                btnUnitCivil2.BackColor = Color.DeepSkyBlue;
            }

            String loadForceCivil = IniFile.GetValue(MainForm.iniPath, "설정", "ForceCivil", "");
            if (loadForceCivil.Equals("LowUnit"))
            {
                foreCivil = "lowUnit";
                btnForeCivil1.BackColor = Color.DeepSkyBlue;
            }
            if (loadForceCivil.Equals("Unit"))
            {
                foreCivil = "unit";
                btnForeCivil2.BackColor = Color.DeepSkyBlue;
            }
            if (loadForceCivil.Equals("Mineral"))
            {
                foreCivil = "mineral";
                btnForeCivil3.BackColor = Color.DeepSkyBlue;
            }
            if (loadForceCivil.Equals("Gas"))
            {
                foreCivil = "gas";
                btnForeCivil4.BackColor = Color.DeepSkyBlue;
            }

            String loadResourceCivil = IniFile.GetValue(MainForm.iniPath, "설정", "ResourceCivil", "");
            if (loadResourceCivil.Equals("Mineral"))
            {
                resourceCivil = "mineral";
                btnResourceCivil1.BackColor = Color.DeepSkyBlue;
            }
            else if (loadResourceCivil.Equals("Gas"))
            {
                resourceCivil = "gas";
                btnResourceCivil2.BackColor = Color.DeepSkyBlue;
            }

            String loadLifeAlarm = IniFile.GetValue(MainForm.iniPath, "설정", "LifeAlarm", "");
            if (loadLifeAlarm.Equals("On"))
            {
                lifeAlarm = true;
                btnLifeAlarmOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadLifeAlarm.Equals("Off"))
            {
                lifeAlarm = false;
                btnLifeAlarmOff.BackColor = Color.DeepSkyBlue;
            }

            String loadAutoSort = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSort", "");
            if (loadAutoSort.Equals("On"))
            {
                unitSort = true;
                btnUnitSortOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadAutoSort.Equals("Off"))
            {
                unitSort = false;
                btnUnitSortOff.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitSave = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSave", "");
            if (loadUnitSave.Equals("On"))
            {
                unitSave = true;
                btnUnitSaveOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadUnitSave.Equals("Off"))
            {
                unitSave = false;
                btnUnitSaveOff.BackColor = Color.DeepSkyBlue;
            }

            String loadCivilSave = IniFile.GetValue(MainForm.iniPath, "설정", "CivilSave", "");
            if (loadCivilSave.Equals("On"))
            {
                civilSave = true;
                btnCivilSaveOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadCivilSave.Equals("Off"))
            {
                civilSave = false;
                btnCivilSaveOff.BackColor = Color.DeepSkyBlue;
            }

            String loadAutoSale = IniFile.GetValue(MainForm.iniPath, "설정", "AutoSale", "");
            if (loadAutoSale.Equals("On"))
            {
                autoSale = true;
                btnAutoSaleOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadAutoSale.Equals("Off"))
            {
                autoSale = false;
                btnAutoSaleOff.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitSale1 = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSale1", "");
            if (loadUnitSale1.Equals("On"))
            {
                unitSale1 = true;
                btnUnitSale1On.BackColor = Color.DeepSkyBlue;
            }
            else if (loadUnitSale1.Equals("Off"))
            {
                unitSale1 = false;
                btnUnitSale1Off.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitSale2 = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSale2", "");
            if (loadUnitSale2.Equals("On"))
            {
                unitSale2 = true;
                btnUnitSale2On.BackColor = Color.DeepSkyBlue;
            }
            else if (loadUnitSale2.Equals("Off"))
            {
                unitSale2 = false;
                btnUnitSale2Off.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitSale3 = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSale3", "");
            if (loadUnitSale3.Equals("On"))
            {
                unitSale3 = true;
                btnUnitSale3On.BackColor = Color.DeepSkyBlue;
            }
            else if (loadUnitSale3.Equals("Off"))
            {
                unitSale3 = false;
                btnUnitSale3Off.BackColor = Color.DeepSkyBlue;
            }

            String loadUnitSale4 = IniFile.GetValue(MainForm.iniPath, "설정", "UnitSale4", "");
            if (loadUnitSale4.Equals("On"))
            {
                unitSale4 = true;
                btnUnitSale4On.BackColor = Color.DeepSkyBlue;
            }
            else if (loadUnitSale4.Equals("Off"))
            {
                unitSale4 = false;
                btnUnitSale4Off.BackColor = Color.DeepSkyBlue;
            }

            String loadTowerSale = IniFile.GetValue(MainForm.iniPath, "설정", "TowerSale", "");
            if (loadTowerSale.Equals("On"))
            {
                towerSale = true;
                btnTowerSaleOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadTowerSale.Equals("Off"))
            {
                towerSale = false;
                btnTowerSaleOff.BackColor = Color.DeepSkyBlue;
            }

            String loadRelicSale = IniFile.GetValue(MainForm.iniPath, "설정", "RelicSale", "");
            if (loadRelicSale.Equals("On"))
            {
                relicSale= true;
                btnRelicSaleOn.BackColor = Color.DeepSkyBlue;
            }
            else if (loadRelicSale.Equals("Off")) 
            { 
                relicSale = false;
                btnRelicSaleOff.BackColor = Color.DeepSkyBlue;
            }
        }
    }
}