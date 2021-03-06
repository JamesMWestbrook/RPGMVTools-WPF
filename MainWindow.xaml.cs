﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.IO.Compression;

using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace RPGMVTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string projectName;
        public static string projectPath;
        public static string BackupTwoPath;
        public static string BackupOnePath;

        public static string RXGCPath;


        public MainWindow()
        {
            InitializeComponent();

            projectName = Properties.Settings.Default.ProjectName;
            ProjectName.Text = projectName;

            projectPath = Properties.Settings.Default.StoredProjectPath;
            ProjectFolderPath.Text = projectPath;

            BackupOnePath = Properties.Settings.Default.StoredBackupOnePath;
            BackupFolderOnePath.Text = BackupOnePath;

            BackupTwoPath = Properties.Settings.Default.StoredBackupTwoPath;
            BackupFolderTwoPath.Text = BackupTwoPath;

            RXGCPath = Properties.Settings.Default.RXGCFolderPath;
            RXGCFolderPath.Text = RXGCPath;

        }


        private void ProjectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            projectPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                projectPath = dialogue.SelectedPath;
            }
            if (projectPath == "Cancel")
            {
                projectPath = "";
            }
            
            ProjectFolderPath.Text = projectPath;
            Properties.Settings.Default.StoredProjectPath = projectPath;

            Properties.Settings.Default.Save();
        }
        private void BackupFolderOneButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true) ProjectPath = File.ReadAllText(openFileDialog.FileName);

            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            BackupOnePath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                BackupOnePath = dialogue.SelectedPath;
            }
            if (BackupOnePath == "Cancel")
            {
                BackupOnePath = "";
            }
            BackupFolderOnePath.Text = BackupOnePath;
            Properties.Settings.Default.StoredBackupOnePath = BackupOnePath;

            Properties.Settings.Default.Save();
        }

        private void BackupFolderTwoButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == true) ProjectPath = File.ReadAllText(openFileDialog.FileName);

            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            BackupTwoPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                BackupTwoPath = dialogue.SelectedPath;
            }
            if (BackupTwoPath == "Cancel")
            {
                BackupTwoPath = "";
            }
            BackupFolderTwoPath.Text = BackupTwoPath;
            Properties.Settings.Default.StoredBackupTwoPath = BackupTwoPath;

            Properties.Settings.Default.Save();
        }


        private void BackupProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (projectPath == "") return;
            System.Diagnostics.Process.Start(projectPath);

            if (BackupOnePath != "")
            {
                if (BackupOnePath != "") System.Diagnostics.Process.Start(BackupOnePath);
                DirectoryInfo BackupOneDir = new DirectoryInfo(BackupOnePath);
                FileInfo[] OpenBackupOne = BackupOneDir.GetFiles();

            }
            if (BackupTwoPath != "")
            {
                if (BackupTwoPath != "") System.Diagnostics.Process.Start(BackupTwoPath);
                DirectoryInfo BackupTwoDir = new DirectoryInfo(BackupTwoPath);
                FileInfo[] OpenBackupTwo = BackupTwoDir.GetFiles();

            }
            DirectoryInfo ProjectFolder = new DirectoryInfo(projectPath);

            FileInfo[] Project = ProjectFolder.GetFiles();

            //DirectoryCopy(projectPath, BackupOnePath, true);
            string BackupZipOne = Path.Combine(BackupOnePath, ProjectName.Text + ".zip");
            string BackupZipTwo = Path.Combine(BackupTwoPath, ProjectName.Text + ".zip");
            if (BackupZipOne != "")
            {
                DeleteZip(BackupZipOne);
            //    ZipFile.CreateFromDirectory(projectPath, BackupOnePath + @"\" + ProjectName + ".zip");
                

            }
            ZipFile.CreateFromDirectory(projectPath, Path.Combine(BackupOnePath, ProjectName.Text + ".zip"));
            if (BackupZipTwo != "")
            {
                DeleteZip(BackupZipTwo);
            }
            ZipFile.CreateFromDirectory(projectPath, Path.Combine(BackupTwoPath, ProjectName.Text + ".zip"));

        }

        private void DeleteZip(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void RXGCFoldeButton_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialogue = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialogue.ShowDialog();
            projectPath = result.ToString();

            if (result == WinForms.DialogResult.OK)
            {
                RXGCPath = dialogue.SelectedPath;
            }

            RXGCFolderPath.Text = RXGCPath;
            Properties.Settings.Default.RXGCFolderPath = RXGCPath;
            Properties.Settings.Default.Save();
        }

        private void MoveGraphics_Click(object sender, RoutedEventArgs e)
        {


            DirectoryInfo RXGCFolder = new DirectoryInfo(RXGCPath);
            DirectoryInfo ProjectFolder = new DirectoryInfo(projectPath);

            FileInfo[] Images = RXGCFolder.GetFiles();

            List<FileInfo> Characters = new List<FileInfo>();

            for (int i = 0; i < Images.Length; i++)
            {
                MoveImage(Images[i], ProjectFolder);
            }

            List<DirectoryInfo> ContainedFolders = RXGCFolder.GetDirectories().ToList();
            foreach (DirectoryInfo directory in ContainedFolders)
            {
                List<FileInfo> innerCharacters = new List<FileInfo>();
                innerCharacters = directory.GetFiles().ToList();
                foreach (FileInfo file in innerCharacters)
                {
                    MoveImage(file, ProjectFolder);
                }
            }

        }
        private void MoveImage(FileInfo file, DirectoryInfo ProjectFolder)
        {
            if (file.Name.Contains("Character.png"))
            {
                string DestinationFile = ProjectFolder + @"\ReturnToOtraGame\img\Characters\" + file.Name;
                if (File.Exists(DestinationFile)) File.Delete(DestinationFile);
                file.MoveTo(DestinationFile);
            }
            else if (file.Name.Contains("Face.png"))
            {
                string DestinationFile = ProjectFolder + @"\ReturnToOtraGame\img\Faces\" + file.Name;
                if (File.Exists(DestinationFile)) File.Delete(DestinationFile);
                file.MoveTo(DestinationFile);
            }
        }

        private void ZipProjectButton_Click(object sender, RoutedEventArgs e)
        {
            ZipFile.CreateFromDirectory(projectPath, @"C:\Users\james\Documents\Output\result.zip");
            //ZipFile.ExtractToDirectory(@"C:\Users\james\Documents\Output\result.zip", @"C:\Users\james\Documents\Output");
        }

        private void NameSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ProjectName = ProjectName.Text;
            Properties.Settings.Default.Save();
        }
    }


}