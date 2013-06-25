using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Utilities
{
	public class AnswerStack<T>
	{
		List<T> _values;
		object _locker = new object();
		int _expectedValues;

		public AnswerStack(int count)
		{
			_values = new List<T>();
			_expectedValues = count;
		}

		public void Push(T value)
		{
			_values.Add(value);
		}

		//TODO: change method`s name
		public Boolean isEnded()
		{
			return _expectedValues == 0 ? true : false;
		}

		public T Pop()
		{
			lock (_locker)
			{
				if (_values.Count != 0)
				{
					var result = _values[_values.Count - 1];
					_values.Remove(result);
					_expectedValues--;
					return result;
				}
				return default(T);
			}
		}

		public void Clear()
		{
			lock (_locker)
			{
				_values = new List<T>();
			}
		}
	}
}
