namespace Feif.IO
{
    public enum FileMode
    {
        /// <summary>
        /// 创建一个新的文件，如果文件已经存在，则清空
        /// </summary>
        CreateNew = 1,
        
        /// <summary>
        /// 打开或创建，如果文件存在则打开，如果文件不存在则创建
        /// </summary>
        OpenOrCreate = 2,
    }
}