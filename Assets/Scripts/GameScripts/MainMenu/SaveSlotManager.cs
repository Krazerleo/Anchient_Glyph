using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AncientGlyph.GameScripts.Constants;
using AncientGlyph.GameScripts.Services.LoggingService;
using UnityEngine;

namespace AncientGlyph.GameScripts.MainMenu
{
    public class SaveSlotManager
    {
        private readonly ILoggingService _logger;
        private const string SaveSlotFolderPattern = "SAVESLOT";
        private const string SaveFolderName = "SAVEDATA";

        public SaveSlotManager(ILoggingService logger)
        {
            _logger = logger;
        }

        public void CreateNewGameSlot(int slotNumber)
        {
            IEnumerable<int> slots = GetGameSlots();
            DirectoryInfo saveFolder = GetSaveSlotsFolder();

            if (slots.Contains(slotNumber))
            {
                _logger.LogTrace("Overriding existing save slot");
                DirectoryInfo saveSlotFolderToDelete = saveFolder.GetDirectories()
                    .First(sd => sd.Name == SaveSlotFolderPattern + slotNumber);

                try
                {
                    Directory.Delete(saveSlotFolderToDelete.ToString(), recursive: true);
                }
                catch (Exception)
                {
                    _logger.LogFatal("Cannot remove folder for overwriting save slot");
                    return;
                }
            }

            DirectoryInfo newSaveSlotFolder = saveFolder.CreateSubdirectory(SaveSlotFolderPattern + slotNumber);
            CopyLevelsToSaveSlotFolder(newSaveSlotFolder);
        }

        public IEnumerable<int> GetGameSlots()
        {
            DirectoryInfo saveSlotsFolder = GetSaveSlotsFolder();
            List<int> resultSlots = new(GameConstants.MaxSaveSlotNumber);

            foreach (DirectoryInfo slotFolder in saveSlotsFolder.GetDirectories())
            {
                if (slotFolder.Name.StartsWith(SaveSlotFolderPattern))
                {
                    // there are fixed slot number - GameConstants.MaxSaveSlotNumber.
                    // The last character in string is number of slot
                    char slotChar = slotFolder.Name[SaveSlotFolderPattern.Length];

                    if (int.TryParse(slotChar.ToString(), out int slotDigit))
                    {
                        resultSlots.Add(slotDigit);
                    }
                    else
                    {
                        _logger.LogError("Invalid save slot folder name");
                    }
                }
                else
                {
                    _logger.LogWarning("Unexpected name of save slot folder");
                }
            }

            return resultSlots;
        }

        private void CopyLevelsToSaveSlotFolder(DirectoryInfo saveSlotFolder)
        {
            DirectoryInfo levelsDirectory = new(Path.Combine(
                Application.streamingAssetsPath,
                FileConstants.StreamingAssetLevelFolderName));

            foreach (FileInfo level in levelsDirectory.GetFiles())
            {
                if (level.Name.EndsWith(".lvl"))
                {
                    File.Copy(level.FullName, saveSlotFolder.FullName);
                }
            }
        }
        
        private static DirectoryInfo GetSaveSlotsFolder()
        {
            string saveLocation = Application.persistentDataPath;
            DirectoryInfo persistentFolder = new(saveLocation);
            DirectoryInfo saveSlotsFolder;

            if (persistentFolder.GetDirectories()
                .Any(f => f.Name == SaveFolderName))
            {
                saveSlotsFolder = persistentFolder.GetDirectories()
                    .First(f => f.Name == SaveFolderName);
            }
            else
            {
                saveSlotsFolder = persistentFolder.CreateSubdirectory(SaveFolderName);
            }

            return saveSlotsFolder;
        }
    }
}