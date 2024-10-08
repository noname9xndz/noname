﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Nona.Component.UploadFile.Models
{
    // This is public only because it's used in a JSInterop method signature,
    // but otherwise is intended as internal
    public class FileListEntryImpl : IFileListEntry
    {
        internal dynamic Owner { get; set; }

        private Stream _stream;

        public event EventHandler OnDataRead;

        public int Id { get; set; }

        public DateTime LastModified { get; set; }

        public string Name { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }

        public string RelativePath { get; set; }

        public Stream Data
        {
            get
            {
                _stream ??= Owner.OpenFileStream(this);
                return _stream;
            }
        }

        public async Task<IFileListEntry> ToImageFileAsync(string format, int maxWidth, int maxHeight)
        {
            var test = Owner;
            return await Owner.ConvertToImageFileAsync(this, format, maxWidth, maxHeight);
            // return new FileListEntryImpl();
        }

        internal void RaiseOnDataRead()
        {
            OnDataRead?.Invoke(this, null);
        }
    }
}