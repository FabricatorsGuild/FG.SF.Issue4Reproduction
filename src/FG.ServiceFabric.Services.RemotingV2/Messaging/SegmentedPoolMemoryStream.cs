// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
namespace FG.ServiceFabric.Services.RemotingV2.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using global::Microsoft.ServiceFabric.Services.Remoting.V2.Messaging;

    internal class SegmentedPoolMemoryStream : Stream
    {
        private readonly IBufferPoolManager bufferPoolManager;

        private int bufferSize;

        private bool canRead;

        private bool canSeek;

        private bool canWrite;

        private IPooledBuffer currentBuffer;

        private int currentBufferOffset;

        private long position;

        private List<IPooledBuffer> writeBuffers;

        public SegmentedPoolMemoryStream(IBufferPoolManager bufferPoolManager)
        {
            this.bufferPoolManager = bufferPoolManager;
            this.Initialize();
        }

        public override bool CanRead => this.canRead;

        public override bool CanSeek => this.canSeek;

        public override bool CanWrite => this.canWrite;

        public override long Length => this.position;

        public override long Position
        {
            get => this.position;
            set => this.position = value;
        }

        public override void Flush()
        {
            // no-op
        }

        public IEnumerable<IPooledBuffer> GetBuffers()
        {
            if (!this.CanWrite)
            {
                throw new NotImplementedException();
            }

            return this.writeBuffers;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            this.position = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (offset + count > buffer.Length)
            {
                throw new ArgumentException("buffer too small", "buffer");
            }

            if (offset < 0)
            {
                throw new ArgumentException("offset must be >= 0", "offset");
            }

            if (count < 0)
            {
                throw new ArgumentException("count must be >= 0", "count");
            }

            var i = this.currentBufferOffset + count;

            if (i <= this.bufferSize)
            {
                Buffer.BlockCopy(buffer, offset, this.currentBuffer.Value.Array, this.currentBufferOffset, count);
                this.currentBuffer.ContentLength += count;
                this.currentBufferOffset += count;
                this.position += count;
                return;
            }

            var bytesLeft = count;

            while (bytesLeft > 0)
            {
                // check for buffer full
                if (this.bufferSize <= this.currentBufferOffset)
                {
                    // Create new buffer and Add to buffer
                    this.currentBuffer = this.bufferPoolManager.TakeBuffer();

                    this.writeBuffers.Add(this.currentBuffer);
                    this.currentBufferOffset = 0;
                }

                var bytesToCopy = this.currentBufferOffset + bytesLeft <= this.bufferSize ? bytesLeft : this.bufferSize - this.currentBufferOffset;

                Buffer.BlockCopy(buffer, offset, this.currentBuffer.Value.Array, this.currentBufferOffset, bytesToCopy);

                this.currentBuffer.ContentLength += bytesToCopy;

                this.position += bytesToCopy;
                offset += bytesToCopy;
                bytesLeft -= bytesToCopy;
                this.currentBufferOffset += bytesToCopy;
            }
        }

        public override void WriteByte(byte value)
        {
            var i = this.currentBufferOffset + 1;

            if (i > this.bufferSize)
            {
                this.currentBuffer = this.bufferPoolManager.TakeBuffer();
                this.writeBuffers.Add(this.currentBuffer);
                this.currentBufferOffset = 0;
            }

            this.currentBuffer.Value.Array[this.currentBufferOffset] = value;
            this.currentBuffer.ContentLength += 1;
            this.position += 1;
        }

        private void Initialize()
        {
            this.canWrite = true;
            this.canRead = false;
            this.canSeek = false;
            this.position = 0;
            this.writeBuffers = new List<IPooledBuffer>(1);
            this.currentBuffer = this.bufferPoolManager.TakeBuffer();
            this.writeBuffers.Add(this.currentBuffer);
            this.bufferSize = this.writeBuffers[0].Value.Count;
            this.currentBufferOffset = 0;
        }
    }
}