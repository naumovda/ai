using System;
using System.Data;

namespace RCEIS.Edits
{
	/// <summary>
	/// ����� ������������ �������� � ������ ����������� ������� ��������������
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
				throw new System.ArgumentException("����� ���� ��������� ���������� ����� � �������");
			}

			if ( code >= _values[ field ].Length )
			{
				throw new System.ArgumentException("����� ���� ���� ��������� ���������� ����� � �������");
			}

			_values[ field ][ code ] = value;
		}

		public bool GetCell( int field, int code )
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("����� ���� ��������� ���������� ����� � �������");
			}

			if ( code >= _values[ field ].Length )
			{
				throw new System.ArgumentException("����� ���� ���� ��������� ���������� ����� � �������");
			}

			return _values[ field ][ code ];
		}

		public bool IsLogicalInference( QLT_EditRule rule )
		{
			if ( rule.Values.Length != _values.Length )
			{
				throw new System.ArgumentException("�� ��������� ���������� ����� � ��������");
			}			

			for ( int i = 0; i < _values.Length; i++ )
			{
				if ( rule.Values[ i ].Length != _values[ i ].Length )
				{
					throw new System.ArgumentException("�� ��������� ���������� ����� ����� � ��������");
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
	/// ��������� ������ ��������������
	/// </summary>
	[Serializable]
	public class QLT_EditRulesCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// �������� ������� � ���������
		/// </summary>
		/// <param name="rule">�������, ����������� � ���������</param>
		public virtual void Add ( QLT_EditRule rule )
		{
			if ( !rule.IsCorrect() )
			{
				throw new System.ArgumentException("����������� ������� �� �������� ����������");
			}

			if ( !rule.IsEssentiallyNew(this) )
			{
				throw new System.ArgumentException("����������� ������� �� �������� ����������� �����");
			}

			if ( this.Count != 0 )
			{
				if ( !rule.EqualStructure(this[0]) )
				{
					throw new System.ArgumentException("����������� ������� ����� ������ ���������");
				}
			}

			this.List.Add( rule );
		}
		
		/// <summary>
		/// ���������� ������� �� ����������� ������
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
		/// �������� ����� ������� �������
		/// </summary>
		/// <param name="genField">����� ������������� ����</param>
		/// <param name="indexes">������ ������, �� ������� ���������� ���������� ����� �������� ����� �������</param>
		/// <param name="addToCollection">��������� �� ������� � ���������</param>
		/// <param name="isCorrect">������� ����, ��� ������� �������� ����������</param>
		/// <param name="isEssentiallyNew">������� ����, ��� ������� �������� ����������� �����</param>
		public QLT_EditRule GetNewImplicitRule( int genField, int [] indexes, bool addToCollection, out bool isCorrect, out bool isEssentiallyNew )
		{
			isCorrect = false;
			isEssentiallyNew = false;
			return null;
		}

		/// <summary>
		/// �������� ��� ����� ������� ������� � �������� �� � ���������
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
		/// �������� ����� ������� ������� � �������� �� � ���������
		/// </summary>
		/// <param name="newRulesCount">���������� ������, ����������� �� ������� �����</param>
		public int GetNewImplicitRules( int newRulesCount )
		{
			//���� �� ���������� ������

			//���� �� ���� ����������� - �������, ������� ...

			//��������, ��� ����� ������� �� �������������� - �.�. ���������� ���� �� ���� ������, ������� ����������� Count - newRulesCount 

			//���� �� ����������� �����		
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
				throw new System.ArgumentException("����� ���� ��������� ���������� ����� � �������");
			}

			_values[ field ] = value;
		}

		public double GetCell( int field )
		{
			if ( field >= _values.Length )
			{
				throw new System.ArgumentException("����� ���� ��������� ���������� ����� � �������");
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

			if (Flag == 0) // ����������� ���������
                return (s < 0);
			else // ����������� �������
				return (s <= 0);
		}

	}

	/// <summary>
	/// ��������� ������ ��������������
	/// </summary>
	[Serializable]
	public class QNT_EditRulesCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// �������� ������� � ���������
		/// </summary>
		/// <param name="rule">�������, ����������� � ���������</param>
		public virtual void Add ( QNT_EditRule rule )
		{
			/*if ( !rule.IsCorrect() )
			{
				throw new System.ArgumentException("����������� ������� �� �������� ����������");
			}

			if ( !rule.IsEssentiallyNew(this) )
			{
				throw new System.ArgumentException("����������� ������� �� �������� ����������� �����");
			}

			if ( this.Count != 0 )
			{
				if ( !rule.EqualStructure(this[0]) )
				{
					throw new System.ArgumentException("����������� ������� ����� ������ ���������");
				}
			}*/

			this.List.Add( rule );
		}
		
		/// <summary>
		/// ���������� ������� �� ����������� ������
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
		/// �������� ����� ������� �������
		/// </summary>
		/// <param name="genField">����� ������������� ����</param>
		/// <param name="indexes">������ ������, �� ������� ���������� ���������� ����� �������� ����� �������</param>
		/// <param name="addToCollection">��������� �� ������� � ���������</param>
		/// <param name="isCorrect">������� ����, ��� ������� �������� ����������</param>
		/// <param name="isEssentiallyNew">������� ����, ��� ������� �������� ����������� �����</param>
		public QNT_EditRule GetNewImplicitRule( int genField, int [] indexes, bool addToCollection, out bool isCorrect, out bool isEssentiallyNew )
		{
			isCorrect = false;
			isEssentiallyNew = false;
			return null;
		}

		/// <summary>
		/// �������� ��� ����� ������� ������� � �������� �� � ���������
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
		/// �������� ����� ������� ������� � �������� �� � ���������
		/// </summary>
		/// <param name="newRulesCount">���������� ������, ����������� �� ������� �����</param>
		public int GetNewImplicitRules( int newRulesCount )
		{
			//���� �� ���������� ������

			//���� �� ���� ����������� - �������, ������� ...

			//��������, ��� ����� ������� �� �������������� - �.�. ���������� ���� �� ���� ������, ������� ����������� Count - newRulesCount 

			//���� �� ����������� �����		
			return 0;
		}
	}
}