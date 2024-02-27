using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
	public record class Todo(
		int? UserId = null,
		int? Id = null,
		string? Title = null,
		bool? Completed = false
		);
}
