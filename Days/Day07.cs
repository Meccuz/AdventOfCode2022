namespace AdventOfCode2022.Days
{
    internal static class Day07
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day07.txt");

            HashSet<Folder> allFolders = new();
            Folder currentFolder = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("$ cd "))
                {
                    var dest = line.Substring(5);
                    if (dest == "..")
                    {
                        currentFolder = currentFolder.Parent;
                    }
                    else
                    {
                        var newFolder = new Folder(dest);
                        if (currentFolder != null)
                        {
                            currentFolder.AddSubFolder(newFolder);
                        }
                        currentFolder = newFolder;
                        allFolders.Add(newFolder);
                    }
                }

                // in this case we have a file
                if (!line.StartsWith("$") && !line.StartsWith("dir"))
                {
                    var fileSize = int.Parse(line.Substring(0, line.IndexOf(" ")));
                    currentFolder.AddFile(fileSize);
                }
            }

            var res1 = 0;
            const int TOTAL_SPACE = 70000000;
            const int SPACE_REQUIRED = 30000000;
            int currentlyOccupied = allFolders.First(x => x.Name == "/").GetTotalSize();
            int spaceToFreeUp = SPACE_REQUIRED - (TOTAL_SPACE - currentlyOccupied);
            var possibleFoldersToDelete = new HashSet<int>();
            foreach (var folder in allFolders)
            {
                var size = folder.GetTotalSize();
                if (size <= 100000)
                {
                    res1 += size;
                }
                if (size > spaceToFreeUp)
                {
                    possibleFoldersToDelete.Add(size);
                }
            }

            Console.WriteLine(res1);
            Console.WriteLine(possibleFoldersToDelete.Min());
            Console.ReadLine();
        }
    }

    internal class Folder
    {
        public string Name { get; }
        public int Size { get; private set; }
        public Folder? Parent { get; private set; }
        public HashSet<Folder> SubFolders { get; }

        public Folder(string name)
        {
            Size = 0;
            SubFolders = new();
            Name = name;
        }

        public void AddSubFolder(Folder subFolder)
        {
            subFolder.SetParent(this);
            SubFolders.Add(subFolder);
        }

        public void SetParent(Folder parent)
        {
            Parent = parent;
        }

        public void AddFile(int size)
        {
            Size += size;
        }

        public int GetTotalSize()
        {
            var size = Size;
            foreach (var subFolder in SubFolders)
            {
                size += subFolder.GetTotalSize();
            }

            return size;
        }
    }
}
