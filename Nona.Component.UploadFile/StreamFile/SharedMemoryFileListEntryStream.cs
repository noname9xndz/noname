using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Nona.Component.UploadFile.Models;

namespace Nona.Component.UploadFile.StreamFile
{
    // This is used on WebAssembly
    internal class SharedMemoryFileListEntryStream : FileListEntryStream
    {
        private static readonly Type MonoWebAssemblyJsRuntimeType
            = Type.GetType("Mono.WebAssembly.Interop.MonoWebAssemblyJSRuntime, Mono.WebAssembly.Interop");

        private static MethodInfo _cachedInvokeUnmarshalledMethodInfo;

        public SharedMemoryFileListEntryStream(IJSRuntime jsRuntime, ElementReference inputFileElement, FileListEntryImpl file)
            : base(jsRuntime, inputFileElement, file)
        {
        }

        public static bool IsSupported(IJSRuntime jsRuntime)
        {
            return MonoWebAssemblyJsRuntimeType != null
                && MonoWebAssemblyJsRuntimeType.IsInstanceOfType(jsRuntime);
        }

        protected override async Task<int> CopyFileDataIntoBuffer(long sourceOffset, byte[] destination, int destinationOffset, int maxBytes, CancellationToken cancellationToken)
        {
            await _jsRuntime.InvokeAsync<string>(
                "NonaComponentUploadFile.ensureArrayBufferReadyForSharedMemoryInterop",
                cancellationToken,
                _inputFileElement,
                _file.Id);

            var methodInfo = GetCachedInvokeUnmarshalledMethodInfo();
            return (int)methodInfo.Invoke(_jsRuntime, new object[]
            {
                "NonaComponentUploadFile.readFileDataSharedMemory",
                new ReadRequest
                {
                    InputFileElementReferenceId = _inputFileElement.Id,
                    FileId = _file.Id,
                    SourceOffset = sourceOffset,
                    Destination = destination,
                    DestinationOffset = destinationOffset,
                    MaxBytes = maxBytes,
                }
            });
        }

        private static MethodInfo GetCachedInvokeUnmarshalledMethodInfo()
        {
            if (_cachedInvokeUnmarshalledMethodInfo == null)
            {
                foreach (var possibleMethodInfo in MonoWebAssemblyJsRuntimeType.GetMethods())
                {
                    if (possibleMethodInfo.Name == "InvokeUnmarshalled" && possibleMethodInfo.GetParameters().Length == 2)
                    {
                        _cachedInvokeUnmarshalledMethodInfo = possibleMethodInfo
                            .MakeGenericMethod(typeof(ReadRequest), typeof(int));
                        break;
                    }
                }

                if (_cachedInvokeUnmarshalledMethodInfo == null)
                {
                    throw new InvalidOperationException("Could not find the 2-param overload of InvokeUnmarshalled");
                }
            }

            return _cachedInvokeUnmarshalledMethodInfo;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct ReadRequest
        {
            [FieldOffset(0)] public string InputFileElementReferenceId;
            [FieldOffset(4)] public int FileId;
            [FieldOffset(8)] public long SourceOffset;
            [FieldOffset(16)] public byte[] Destination;
            [FieldOffset(20)] public int DestinationOffset;
            [FieldOffset(24)] public int MaxBytes;
        }
    }
}