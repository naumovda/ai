using System;
using System.Data;

namespace RCEIS.Edits
{
	/// <summary>
	/// Класс инкапсулирет свойства и методы логического правила редактирования
	/// </summary>
	[Serializable]
	public class QLT_EditRule  
	{
		private bool [][] _values;

		public bool [][] Values
		{
			get
			{
				return this._values;
			}
		}

		public void Initialize(int [] size)
		{
			int n = size.Length;

			_values = new bool[n][];

			for (int i = 0; i < n; i++)
				_values[i] = new bool[ size[i] ];
		}

		public void SetCell(int field, int code, bool value)
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("Номер поля превышает количество полей в правиле");
			}

			if ( code >= _values[ field ].Length )
			{
				throw new System.ArgumentException("Номер кода поля превышает количество полей в правиле");
			}

			_values[ field ][ code ] = value;
		}

		public bool GetCell( int field, int code )
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("Номер поля превышает количество полей в правиле");
			}

			if ( code >= _values[ field ].Length )
			{
				throw new System.ArgumentException("Номер кода поля превышает количество полей в правиле");
			}

			return _values[ field ][ code ];
		}

		public bool IsLogicalInference( QLT_EditRule rule )
		{
			if ( rule.Values.Length != _values.Length )
			{
				throw new System.ArgumentException("Не совпадает количество полей в правилах");
			}			

			for ( int i = 0; i < _values.Length; i++ )
			{
				if ( rule.Values[ i ].Length != _values[ i ].Length )
				{
					throw new System.ArgumentException("Не совпадает количество кодов полей в правилах");
				}			
				
				for ( int j = 0; j < _values[ i ].Length; j++ )
				{
					if ( _values[ i ][ j ] && !rule.Values[ i ][ j ])
						return false;
				}
			}

			return true;
		}
		
		public bool IsEssentiallyNew( QLT_EditRulesCollection rulesCol )
		{
			for ( int i = 0; i < rulesCol.Count; i++ ) 
			{
				if ( IsLogicalInference(rulesCol[ i ]) )
				{
					return false;
				}
			}
			
			return true;
		}

		public bool IsCorrect( )
		{
			for ( int i = 0; i < _values.Length; i++ )
			{
				bool correct = false;
								
				for ( int j = 0; j < _values[ i ].Length; j++ )
				{
					correct = correct || _values[ i ][ j ];
				}

				if ( !correct) return false;
			}

			return true;
		}

		public bool CheckValue( bool [][] data )
		{
			for ( int i = 0; i < _values.Length; i++ )
			{					
				for ( int j = 0; j < _values[ i ].Length; j++ )
				{
					if (data[ i ][ j ] && !_values[ i ][ j ])
						return true;
				}
			}

			return false;
		}

		public bool EqualStructure( QLT_EditRule rule )
		{
			if ( rule.Values.Length != _values.Length )
			{
				return false;
			}			

			for ( int i = 0; i < _values.Length; i++ )
			{
				if ( rule.Values[ i ].Length != _values[ i ].Length )
				{
					return false;
				}											
			}

			return true;		
		}
	}


	/// <summary>
	/// Коллекция правил редактирования
	/// </summary>
	[Serializable]
	public class QLT_EditRulesCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// Добавить правило в коллекцию
		/// </summary>
		/// <param name="rule">Правило, добавляемое в коллекцию</param>
		public virtual void Add ( QLT_EditRule rule )
		{
			if ( !rule.IsCorrect() )
			{
				throw new System.ArgumentException("Добавляемое правило не является корректным");
			}

			if ( !rule.IsEssentiallyNew(this) )
			{
				throw new System.ArgumentException("Добавляемое правило не является существенно новым");
			}

			if ( this.Count != 0 )
			{
				if ( !rule.EqualStructure(this[0]) )
				{
					throw new System.ArgumentException("Добавляемое правило имеет другую структуру");
				}
			}

			this.List.Add( rule );
		}
		
		/// <summary>
		/// Возвратить правило по порядковому номеру
		/// </summary>
		public virtual QLT_EditRule this[ int Index ] 
		{ 
			get 
			{ 
				return ( QLT_EditRule )this.List[ Index ];            
			}      
			set
			{
			}
		} 

		/// <summary>
		/// Получить новое неявное правило
		/// </summary>
		/// <param name="genField">Номер генерирующего поля</param>
		/// <param name="indexes">Номера правил, из которых логическим следствием будет получено новое правило</param>
		/// <param name="addToCollection">Добавлять ли правило в коллекцию</param>
		/// <param name="isCorrect">Признак того, что правило является корректным</param>
		/// <param name="isEssentiallyNew">Признак того, что правило является существенно новым</param>
		public QLT_EditRule GetNewImplicitRule( int genField, int [] indexes, bool addToCollection, out bool isCorrect, out bool isEssentiallyNew )
		{
			isCorrect = false;
			isEssentiallyNew = false;
			return null;
		}

		/// <summary>
		/// Получить все новые неявные правила и добавить их в коллекцию
		/// </summary>
		public void GetAllNewImplicitRules(  )
		{
			int n = this.Count;
			do
			{
				n = GetNewImplicitRules( n );
			}
			while ( n != 0 );
		}

		/// <summary>
		/// Получить новые неявные правила и добавить их в коллекцию
		/// </summary>
		/// <param name="newRulesCount">Количество правил, добавленных на прошлом этапе</param>
		public int GetNewImplicitRules( int newRulesCount )
		{
			//Цикл по количеству правил

			//Цикл по всем комбинациям - двойкам, тройкам ...

			//Проверка, что такое правило не генерировалось - т.е. существует хотя бы один индекс, который превосходит Count - newRulesCount 

			//Цикл по генериующим полям		
			return 0;
		}

		public bool [][] GetFilter( Questionarie qst, bool [] beImputed, Value val )
		{
			int c_count = beImputed.Length;

			bool [][] val_qlt = val.GetQLT(qst);

			bool [][] filter = new bool[ c_count ][];

			for(int i=0; i<c_count; i++)
			{
				filter[ i ] = new bool[val_qlt[ i ].Length];

				for(int j=0; j<val_qlt[ i ].Length; j++)
					filter[ i ][ j ] = true;
			}

			for(int i=0; i<c_count; i++)
			{
				if ( beImputed[ i ] )
					continue;
                foreach(QLT_EditRule qlt_er in this)				
				{
					for(int j=0; j<val_qlt[ i ].Length; j++)
						filter[ i ][ j ] = filter[ i ][ j ] && qlt_er.Values[ i ][ j ];
				}
			}

			return filter;
		}

		public bool ApllyFilter( Questionarie qst, bool [][] filter, Value val )
		{
			bool [][] val_qlt = val.GetQLT(qst);

			int c_count = val_qlt.GetUpperBound(0);

			for(int i=0; i<c_count; i++)
			{
				for(int j=0; j<val_qlt[ i ].Length; j++)
					if ( val_qlt[ i ][ j ] && !filter[ i ][ j ] )
						return false;
			}

			return true;
		}

	}

	public class QNT_EditRule  
	{
		private byte flag;

		private double [] _values;

		public double [] Values
		{
			get
			{
				return this._values;
			}			
		}

		public byte Flag
		{
			get {return flag;}
			set {flag = value;}
		}

		public void Initialize(int size)
		{
			_values = new double[size];
		}

		public void SetCell(int field, double value)
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("Номер поля превышает количество полей в правиле");
			}

			_values[ field ] = value;
		}

		public double GetCell( int field )
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("Номер поля превышает количество полей в правиле");
			}

			return _values[ field ];
		}

		public bool CheckValue( double [] data )
		{
			double s = 0;

			for ( int i = 0; i < _values.Length; i++ )
			{					
				s += data[ i ] * _values[ i ];
			}

			if (Flag == 0) // неравенство нестрогое
                return (s < 0);
			else // неравенство строгое
				return (s <= 0);
		}

	}

	/// <summary>
	/// Коллекция правил редактирования
	/// </summary>
	[Serializable]
	public class QNT_EditRulesCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// Добавить правило в коллекцию
		/// </summary>
		/// <param name="rule">Правило, добавляемое в коллекцию</param>
		public virtual void Add ( QNT_EditRule rule )
		{
			/*if ( !rule.IsCorrect() )
			{
				throw new System.ArgumentException("Добавляемое правило не является корректным");
			}

			if ( !rule.IsEssentiallyNew(this) )
			{
				throw new System.ArgumentException("Добавляемое правило не является существенно новым");
			}

			if ( this.Count != 0 )
			{
				if ( !rule.EqualStructure(this[0]) )
				{
					throw new System.ArgumentException("Добавляемое правило имеет другую структуру");
				}
			}*/

			this.List.Add( rule );
		}
		
		/// <summary>
		/// Возвратить правило по порядковому номеру
		/// </summary>
		public virtual QNT_EditRule this[ int Index ] 
		{ 
			get 
			{ 
				return ( QNT_EditRule )this.List[ Index ];            
			}      
			set
			{
			}
		} 

		/// <summary>
		/// Получить новое неявное правило
		/// </summary>
		/// <param name="genField">Номер генерирующего поля</param>
		/// <param name="indexes">Номера правил, из которых логическим следствием будет получено новое правило</param>
		/// <param name="addToCollection">Добавлять ли правило в коллекцию</param>
		/// <param name="isCorrect">Признак того, что правило является корректным</param>
		/// <param name="isEssentiallyNew">Признак того, что правило является существенно новым</param>
		public QNT_EditRule GetNewImplicitRule( int genField, int [] indexes, bool addToCollection, out bool isCorrect, out bool isEssentiallyNew )
		{
			isCorrect = false;
			isEssentiallyNew = false;
			return null;
		}

		/// <summary>
		/// Получить все новые неявные правила и добавить их в коллекцию
		/// </summary>
		public void GetAllNewImplicitRules(  )
		{
			int n = this.Count;
			do
			{
				n = GetNewImplicitRules( n );
			}
			while ( n != 0 );
		}

		/// <summary>
		/// Получить новые неявные правила и добавить их в коллекцию
		/// </summary>
		/// <param name="newRulesCount">Количество правил, добавленных на прошлом этапе</param>
		public int GetNewImplicitRules( int newRulesCount )
		{
			//Цикл по количеству правил

			//Цикл по всем комбинациям - двойкам, тройкам ...

			//Проверка, что такое правило не генерировалось - т.е. существует хотя бы один индекс, который превосходит Count - newRulesCount 

			//Цикл по генериующим полям		
			return 0;
		}
	}
}