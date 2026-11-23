namespace Shared
{
    /// <summary>
    /// DTO бренда
    /// </summary>
    public class BrandDto
    {
        /// <summary>Идентификатор (0 для нового бренда)</summary>
        public int Id { get; set; }

        /// <summary>Название</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Страна</summary>
        public string Country { get; set; } = string.Empty;
    }
}
