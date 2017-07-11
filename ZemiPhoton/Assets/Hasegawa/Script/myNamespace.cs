using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myNamespace : MonoBehaviour {
}
//******************************************************************//
// 名前空間 : 検証用の関数のため名前空間でどこにでも使えるようにする//
//******************************************************************//
namespace Verification{
	// 挨拶
	public class hello{	public static void HellWorld(){Debug.Log ("Hello World");}}


	public class verifi{
		//******************************************************************//
		// ビット値を返す													//
		//******************************************************************//
		// int
		public static int BitConversion(int value){
			// ビット値変換
			int bit = int.Parse(Convert.ToString (value, 2).PadLeft (32, '0'));
			return bit;
		}
		// float
		public static int BitConversion(float value){
			// ビット値変換
			int bit = int.Parse(Convert.ToString ((int)value, 2).PadLeft (32, '0'));
			return bit;
		}
		// double
		public static int BitConversion(double value){
			// ビット値変換
			int bit = int.Parse(Convert.ToString ((int)value, 2).PadLeft (32, '0'));
			return bit;
		}
		//******************************************************************//
		//******************************************************************//


		//******************************************************************//
		// ビット値の計算													//
		//******************************************************************//
		// int
		public static int BitCalculation(int value){
			// カウント
			int count = 0;
			// ビット格納
			int bit = 0;
			// ビット取り出し
			bit = BitConversion (value);

			for (int i = 0; i < 32; i++) {
				// ビット数代入
				if (bit == 1)
					count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		// float
		public static int BitCalculation(float value){
			// カウント
			int count = 0;
			// ビット格納
			int bit = 0;
			// ビット取り出し
			bit = BitConversion (value);

			for (int i = 0; i < 32; i++) {
				// ビット数代入
				if (bit == 1)
					count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		// double
		public static int BitCalculation(double value){
			// カウント
			int count = 0;
			// ビット格納
			int bit = 0;
			// ビット取り出し
			bit = BitConversion (value);

			for (int i = 0; i < 32; i++) {
				// ビット数代入
				if (bit == 1)
					count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		//******************************************************************//
		//******************************************************************//
	}

}
//******************************************************************//
//******************************************************************//
