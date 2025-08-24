﻿namespace ITIManagement.BLL.Pagination
{
	public class PageResult<T> where T : class
	{
		public IEnumerable<T> Items { get; set; } = new List<T>();
		public int Page { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
		public bool HasPrevious => Page > 1;
		public bool HasNext => Page < TotalPages;
	}
}
