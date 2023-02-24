using System;
namespace _1.Odev.Base
{
	public class CommonResponse<TEntity>
	{
		public CommonResponse(TEntity entity)
		{
			Data = entity;
		}
		public CommonResponse(string error)
		{
			Error = error;
			Success = false;
		}

		public TEntity Data { get; set; }
		public bool Success { get; set; } = true;
		public string Error { get; set; }
	}
}

