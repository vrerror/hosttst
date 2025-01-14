﻿
namespace TST.Core.Common
{
    public static class Role
    {
        public const string System = nameof(System);
    }

    public static class UploadFolder
    {
        public const string Client = nameof(Client);
        public const string Performance = nameof(Performance);
        public const string Slide = nameof(Slide);
        public const string Welcome = nameof(Welcome);

    }

    public static class SecretKey
    {
        public static readonly byte[] Default = [202, 75, 15, 116, 208, 124, 25, 103, 191, 70, 136, 190, 169, 187, 171, 103, 38, 17, 27, 221, 69, 72, 82, 144, 177, 191, 189, 230, 20, 236, 250, 194];
    }
}
