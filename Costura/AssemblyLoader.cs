// Decompiled with JetBrains decompiler
// Type: Costura.AssemblyLoader
// Assembly: WPFTest, Version=1.0.5.6, Culture=neutral, PublicKeyToken=null
// MVID: D95E3EB1-EF84-499C-A069-258C3C24EF0D
// Assembly location: C:\Program Files (x86)\FlydigiPcSpace\FlydigiPcSpace.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
    [CompilerGenerated]
    internal static class AssemblyLoader
    {
        private static object nullCacheLock = new object();
        private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();
        private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();
        private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();
        private static int isAttached;

        private static string CultureToString(CultureInfo culture) => culture == null ? "" : culture.Name;

        private static Assembly ReadExistingAssembly(AssemblyName name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName name1 = assembly.GetName();
                // ISSUE: reference to a compiler-generated method
                // ISSUE: reference to a compiler-generated method
                if (string.Equals(name1.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name1.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
                    return assembly;
            }
            return (Assembly)null;
        }

        private static void CopyTo(Stream source, Stream destination)
        {
            byte[] buffer = new byte[81920];
            int count;
            while ((count = source.Read(buffer, 0, buffer.Length)) != 0)
                destination.Write(buffer, 0, count);
        }

        private static Stream LoadStream(string fullName)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            if (!fullName.EndsWith(".compressed"))
                return executingAssembly.GetManifestResourceStream(fullName);
            using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
            {
                using (DeflateStream source = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
                {
                    MemoryStream destination = new MemoryStream();
                    // ISSUE: reference to a compiler-generated method
                    AssemblyLoader.CopyTo((Stream)source, (Stream)destination);
                    destination.Position = 0L;
                    return (Stream)destination;
                }
            }
        }

        private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
        {
            string fullName;
            // ISSUE: reference to a compiler-generated method
            return resourceNames.TryGetValue(name, out fullName) ? AssemblyLoader.LoadStream(fullName) : (Stream)null;
        }

        private static byte[] ReadStream(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        private static Assembly ReadFromEmbeddedResources(
          Dictionary<string, string> assemblyNames,
          Dictionary<string, string> symbolNames,
          AssemblyName requestedAssemblyName)
        {
            string name = requestedAssemblyName.Name.ToLowerInvariant();
            if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
                name = requestedAssemblyName.CultureInfo.Name + "." + name;
            byte[] rawAssembly;
            // ISSUE: reference to a compiler-generated method
            using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, name))
            {
                if (stream == null)
                    return (Assembly)null;
                // ISSUE: reference to a compiler-generated method
                rawAssembly = AssemblyLoader.ReadStream(stream);
            }
            // ISSUE: reference to a compiler-generated method
            using (Stream stream = AssemblyLoader.LoadStream(symbolNames, name))
            {
                if (stream != null)
                {
                    // ISSUE: reference to a compiler-generated method
                    byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream);
                    return Assembly.Load(rawAssembly, rawSymbolStore);
                }
            }
            return Assembly.Load(rawAssembly);
        }

        public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            lock (AssemblyLoader.nullCacheLock)
            {
                // ISSUE: reference to a compiler-generated field
                if (AssemblyLoader.nullCache.ContainsKey(e.Name))
                    return (Assembly)null;
            }
            AssemblyName assemblyName = new AssemblyName(e.Name);
            // ISSUE: reference to a compiler-generated method
            Assembly assembly1 = AssemblyLoader.ReadExistingAssembly(assemblyName);
            if (assembly1 != (Assembly)null)
                return assembly1;
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            Assembly assembly2 = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
            if (assembly2 == (Assembly)null)
            {
                // ISSUE: reference to a compiler-generated field
                lock (AssemblyLoader.nullCacheLock)
                {
                    // ISSUE: reference to a compiler-generated field
                    AssemblyLoader.nullCache[e.Name] = true;
                }
                if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
                    assembly2 = Assembly.Load(assemblyName);
            }
            return assembly2;
        }

        static AssemblyLoader()
        {
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("deviceid", "costura.deviceid.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("hidsharp", "costura.hidsharp.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.symbolNames.Add("hidsharp", "costura.hidsharp.pdb.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("libusbdotnet.libusbdotnet", "costura.libusbdotnet.libusbdotnet.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.symbolNames.Add("libusbdotnet.libusbdotnet", "costura.libusbdotnet.libusbdotnet.pdb.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("sharpdx", "costura.sharpdx.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.symbolNames.Add("sharpdx", "costura.sharpdx.pdb.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("sharpdx.xinput", "costura.sharpdx.xinput.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.symbolNames.Add("sharpdx.xinput", "costura.sharpdx.xinput.pdb.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.assemblyNames.Add("wpfanimatedgif", "costura.wpfanimatedgif.dll.compressed");
            // ISSUE: reference to a compiler-generated field
            AssemblyLoader.symbolNames.Add("wpfanimatedgif", "costura.wpfanimatedgif.pdb.compressed");
        }

        public static void Attach()
        {
            // ISSUE: reference to a compiler-generated field
            if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
                return;
            AppDomain.CurrentDomain.AssemblyResolve += (ResolveEventHandler)((sender, e) =>
            {
                // ISSUE: reference to a compiler-generated field
                lock (AssemblyLoader.nullCacheLock)
                {
                    // ISSUE: reference to a compiler-generated field
                    if (AssemblyLoader.nullCache.ContainsKey(e.Name))
                        return (Assembly)null;
                }
                AssemblyName assemblyName = new AssemblyName(e.Name);
                // ISSUE: reference to a compiler-generated method
                Assembly assembly1 = AssemblyLoader.ReadExistingAssembly(assemblyName);
                if (assembly1 != (Assembly)null)
                    return assembly1;
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated method
                Assembly assembly2 = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
                if (assembly2 == (Assembly)null)
                {
                    // ISSUE: reference to a compiler-generated field
                    lock (AssemblyLoader.nullCacheLock)
                    {
                        // ISSUE: reference to a compiler-generated field
                        AssemblyLoader.nullCache[e.Name] = true;
                    }
                    if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
                        assembly2 = Assembly.Load(assemblyName);
                }
                return assembly2;
            });
        }
    }
}
