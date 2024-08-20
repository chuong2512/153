using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#if !UNITY_4_6
//using UnityEditor.iOS.Xcode;
#endif
#endif
using System.IO;
using System;

namespace VascoGamesEditor.Admob
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    public class AdmobPostprocessor : MonoBehaviour
    {
        private const string _lib_name = "admob-ad-plugin";
        private const string _dir_name = "Ads";
        private const string _product_name = "AdmobAd";
        private static string _applicationDataPath = Application.dataPath;
        private static string[] _unused_files;
        private static string[] _post_process_file;
        private static string[] _permissions;
        private static string[] _capabilities;

        [PostProcessBuild(999)]
        public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
        {
			#if UNITY_4_6
			if(buildTarget == BuildTarget.iPhone)
				PatchIOSConfiguration(false);
			#else
				
			if(buildTarget == BuildTarget.iOS)
				PatchIOSConfiguration(false);
			#endif
		}

        static AdmobPostprocessor()
        {
            string[] strArray1 = new string[2];
            int index1 = 0;
            string str1 = "Assets/Plugins/Android/GoogleAdMobAdsSdk.jar";
            strArray1[index1] = str1;
            int index2 = 1;
            string str2 = "Assets/Plugins/Android/res/values/np_admob_ad.xml";
            strArray1[index2] = str2;
            AdmobPostprocessor._unused_files = strArray1;
            string[] strArray2 = new string[21];
            int index3 = 0;
            string str3 = "#!/usr/bin/env python";
            strArray2[index3] = str3;
            int index4 = 1;
            string str4 = "import sys, os.path";
            strArray2[index4] = str4;
            int index5 = 2;
            string str5 = "install_path = sys.argv[1]";
            strArray2[index5] = str5;
            int index6 = 3;
            string str6 = "target_platform = sys.argv[2]";
            strArray2[index6] = str6;
            int index7 = 4;
            string str7 = "if target_platform != \"iPhone\": sys.exit()";
            strArray2[index7] = str7;
            int index8 = 5;
            string str8 = "mod_path = os.path.join('.', 'Assets', 'Editor', 'NeatPlug', 'Common', 'lib')";
            strArray2[index8] = str8;
            int index9 = 6;
            string str9 = "sys.path.append(mod_path)";
            strArray2[index9] = str9;
            int index10 = 7;
            string str10 = "from mod_pbxproj import XcodeProject";
            strArray2[index10] = str10;
            int index11 = 8;
            string str11 = "project_path = os.path.join(install_path, 'Unity-iPhone.xcodeproj', 'project.pbxproj')";
            strArray2[index11] = str11;
            int index12 = 9;
            string str12 = "project = XcodeProject.Load(project_path)";
            strArray2[index12] = str12;
            int index13 = 10;
            string str13 = "project.add_other_ldflags('-ObjC')";
            strArray2[index13] = str13;
            int index14 = 11;
            string str14 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/StoreKit.framework', tree='SDKROOT')";
            strArray2[index14] = str14;
            int index15 = 12;
            string str15 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/AdSupport.framework', tree='SDKROOT', weak=True)";
            strArray2[index15] = str15;
            int index16 = 13;
            string str16 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/MessageUI.framework', tree='SDKROOT', weak=True)";
            strArray2[index16] = str16;
            int index17 = 14;
            string str17 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/CoreTelephony.framework', tree='SDKROOT', weak=True)";
            strArray2[index17] = str17;
            int index18 = 15;
            string str18 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/Security.framework', tree='SDKROOT', weak=True)";
            strArray2[index18] = str18;
            int index19 = 16;
            string str19 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/SystemConfiguration.framework', tree='SDKROOT', weak=True)";
            strArray2[index19] = str19;
            int index20 = 17;
            string str20 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/EventKit.framework', tree='SDKROOT', weak=True)";
            strArray2[index20] = str20;
            int index21 = 18;
            string str21 = "project.add_file_if_doesnt_exist('System/Library/Frameworks/EventKitUI.framework', tree='SDKROOT', weak=True)";
            strArray2[index21] = str21;
            int index22 = 19;
            string str22 = "if project.modified: project.backup()";
            strArray2[index22] = str22;
            int index23 = 20;
            string str23 = "project.saveFormat3_2()";
            strArray2[index23] = str23;
            AdmobPostprocessor._post_process_file = strArray2;
            string[] strArray3 = new string[2];
            int index24 = 0;
            string str24 = "android.permission.INTERNET";
            strArray3[index24] = str24;
            int index25 = 1;
            string str25 = "android.permission.ACCESS_NETWORK_STATE";
            strArray3[index25] = str25;
            AdmobPostprocessor._permissions = strArray3;
            string[] strArray4 = new string[4];
            int index26 = 0;
            string str26 = "ID_CAP_NETWORKING";
            strArray4[index26] = str26;
            int index27 = 1;
            string str27 = "ID_CAP_MEDIALIB_AUDIO";
            strArray4[index27] = str27;
            int index28 = 2;
            string str28 = "ID_CAP_MEDIALIB_PLAYBACK";
            strArray4[index28] = str28;
            int index29 = 3;
            string str29 = "ID_CAP_WEBBROWSERCOMPONENT";
            strArray4[index29] = str29;
            AdmobPostprocessor._capabilities = strArray4;
        }

        public static string[] PostProcessFile()
        {
            return AdmobPostprocessor._post_process_file;
        }

        public static string ProductName()
        {
            return AdmobPostprocessor._product_name;
        }

        public static string[] GetUnusedFiles()
        {
            return AdmobPostprocessor._unused_files;
        }

        public static string[] GetPermissions()
        {
            return AdmobPostprocessor._permissions;
        }

        public static string[] GetCapabilities()
        {
            return AdmobPostprocessor._capabilities;
        }

        public static void PatchIOSConfiguration(bool shouldShowDialog)
        {
            if (!AdmobPostprocessor.InstallXcodeProjectModifier())
                return;

            string path1 = Path.Combine(AdmobPostprocessor._applicationDataPath, "Editor");
            string str = Path.Combine(path1, "PostprocessBuildPlayer");
            string fileName = Path.Combine(path1, "NPDisabled_PostprocessBuildPlayer");

            if(new FileInfo(str).Exists)
            {
                if(!new FileInfo(fileName).Exists)
                    AssetDatabase.CopyAsset("Assets/Editor/PostprocessBuildPlayer", "Assets/Editor/NPDisabled_PostprocessBuildPlayer");

                AssetDatabase.DeleteAsset("Assets/Editor/PostprocessBuildPlayer");
            }

            AdmobPostprocessor.CreateCommonPostProcessor(str);
            AdmobPostprocessor.CreatePostProcessor(str + "_NP" + AdmobPostprocessor.ProductName(), shouldShowDialog);
        }

        public static bool InstallXcodeProjectModifier()
        {
            string path1 = Path.Combine(Path.Combine(Path.Combine(Path.Combine(AdmobPostprocessor._applicationDataPath, "Editor"), "NeatPlug"), "Common"), "lib");
            if (System.IO.File.Exists(Path.Combine(path1, "mod_pbxproj.py")) && System.IO.File.Exists(Path.Combine(path1, "biplist.py")) && System.IO.File.Exists(Path.Combine(path1, "six.py")))
                return true;

            EditorUtility.DisplayDialog("NeatPlug - Operation Failed", "Some dependencies are missing, please reimport the plugin package and try again.", "OK");
            return false;
        }

        public static void CreateCommonPostProcessor(string path)
        {
            string[] strArray = new string[11];
            int index1 = 0;
            string str1 = "#!/usr/bin/env python";
            strArray[index1] = str1;
            int index2 = 1;
            string str2 = "import sys, os.path, subprocess, fnmatch, re";
            strArray[index2] = str2;
            int index3 = 2;
            string str3 = "editor_path = os.path.join('.', 'Assets', 'Editor')";
            strArray[index3] = str3;
            int index4 = 3;
            string str4 = "editor_files = [i for i in os.listdir(editor_path) if os.path.isfile(os.path.join(editor_path, i)) and not i.endswith('.meta')]";
            strArray[index4] = str4;
            int index5 = 4;
            string str5 = "editor_exec_files = [os.path.join(editor_path, j) for j in editor_files if re.match(fnmatch.translate('PostprocessBuildPlayer_*'), j, re.IGNORECASE)]";
            strArray[index5] = str5;
            int index6 = 5;
            string str6 = "for exec_file in editor_exec_files:";
            strArray[index6] = str6;
            int index7 = 6;
            string str7 = "  try:";
            strArray[index7] = str7;
            int index8 = 7;
            string str8 = "    sys.argv[0] = exec_file";
            strArray[index8] = str8;
            int index9 = 8;
            string str9 = "    subprocess.call(sys.argv)";
            strArray[index9] = str9;
            int index10 = 9;
            string str10 = "  except OSError as e:";
            strArray[index10] = str10;
            int index11 = 10;
            string str11 = "    print(exec_file + ' ' + str(e.errno))";
            strArray[index11] = str11;
            string[] contents = strArray;
            System.IO.File.WriteAllLines(path, contents);
            AdmobPostprocessor.SetFileExecutable(path);
        }

        public static void CreatePostProcessor(string path, bool showDialog)
        {
            if (path == null)
                return;

            bool flag = false;
            string[] contents = AdmobPostprocessor.PostProcessFile();
            if (contents == null || contents.Length == 0)
                return;

            if (new FileInfo(path).Exists)
            {
                StreamReader streamReader = (StreamReader)null;
                string str1 = (string)null;
                try
                {
                    streamReader = new StreamReader(path);
                    if (streamReader != null)
                        str1 = streamReader.ReadToEnd();
                }
                finally
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }
                }
                if (str1 != null)
                {
                    string str2 = str1;
                    string[] separator = new string[1];
                    int index1 = 0;
                    string str3 = "\n";
                    separator[index1] = str3;
                    int num = 0;
                    string[] strArray = str2.Split(separator, (StringSplitOptions)num);
                    if (strArray.Length != contents.Length + 1)
                    {
                        flag = true;
                    }
                    else
                    {
                        for (int index2 = 0; index2 < contents.Length; ++index2)
                        {
                            if (contents[index2] != strArray[index2])
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                else
                    flag = true;
            }
            else
                flag = true;

            if (flag)
            {
                AssetDatabase.DeleteAsset("Assets/Editor/PostprocessBuildPlayer_NP" + AdmobPostprocessor.ProductName());
                System.IO.File.WriteAllLines(path, contents);
                AdmobPostprocessor.SetFileExecutable(path);
                AdmobPostprocessor.ShowEditorMessage("NeatPlug", "iOS Configuration has been updated to support " + AdmobPostprocessor.ProductName() + " Plugin.", "OK", showDialog, true);
            }
            else
                AdmobPostprocessor.ShowEditorMessage("NeatPlug", "iOS Configuration appears to be OK with " + AdmobPostprocessor.ProductName() + " Plugin support.", "OK", showDialog, false);
        }

        public static void ShowEditorMessage(string title, string message, string okButton, bool showDialog, bool showInLog)
        {
            if (showDialog)
                EditorUtility.DisplayDialog(title, message, okButton);
            if (!showInLog)
                return;
            string[] strArray = new string[7];
            int index1 = 0;
            string str1 = "[";
            strArray[index1] = str1;
            int index2 = 1;
            string str2 = title;
            strArray[index2] = str2;
            int index3 = 2;
            string str3 = "] ";
            strArray[index3] = str3;
            int index4 = 3;
            string str4 = message;
            strArray[index4] = str4;
            int index5 = 4;
            string str5 = " (@ ";
            strArray[index5] = str5;
            int index6 = 5;
            string str6 = DateTime.Now.ToString();
            strArray[index6] = str6;
            int index7 = 6;
            string str7 = ")";
            strArray[index7] = str7;
            Console.WriteLine(string.Concat(strArray));
        }

        public static void SetFileExecutable(string path)
        {
            if (!new FileInfo(path).Exists)
                return;

            System.IO.File.SetAttributes(path, System.IO.File.GetAttributes(path) | (FileAttributes) (-2147483648)); 
        }
    }
#endif
}