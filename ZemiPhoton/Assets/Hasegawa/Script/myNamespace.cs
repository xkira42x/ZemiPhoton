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
		// Vector3
		public static Vector3 BitConversion(Vector3 value){
			// ビット値変換
			Vector3 bit;
			bit.x = float.Parse(Convert.ToString ((int)value.x, 2).PadLeft (32, '0'));
			bit.y = float.Parse(Convert.ToString ((int)value.y, 2).PadLeft (32, '0'));
			bit.z = float.Parse(Convert.ToString ((int)value.z, 2).PadLeft (32, '0'));
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
				if (bit == 1)count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		// float
		public static int BitCalculation(float value){
			// カウント
			int count = 1;
			// ビット格納
			int bit = 0;
			// ビット取り出し
			bit = BitConversion (value);

			for (int i = 0; i < 32; i++) {
				// ビット数代入
				if (bit == 1)count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		// double
		public static int BitCalculation(double value){
			// カウント
			int count = 1;
			// ビット格納
			int bit = 0;
			// ビット取り出し
			bit = BitConversion (value);

			for (int i = 0; i < 32; i++) {
				// ビット数代入
				if (bit == 1)count = i;
				// シフト
				bit = bit/10;
			}
			return count + 1;
		}
		// Vector3
		public static Vector3 BitCalculation(Vector3 value){
			// カウント
			Vector3 count = Vector3.zero;
			// 既存の関数を借りる
			count.x = BitCalculation (value.x);
			count.y = BitCalculation (value.y);
			count.z = BitCalculation (value.z);
			return count;
		}
		//******************************************************************//
		//******************************************************************//


		//******************************************************************//
		// 結果の表示														//
		//******************************************************************//
		// int
		public static void ShowResult(int value){
			Debug.Log ("Result(int) : " + BitCalculation (value) + "bit   0x" + BitConversion (value));
		}
		// float
		public static void ShowResult(float value){
			Debug.Log ("Result(float) : " + BitCalculation (value) + "bit   0x" + BitConversion (value));
		}
		// double
		public static void ShowResult(double value){
			Debug.Log ("Result(double) : " + BitCalculation (value) + "bit   0x" + BitConversion (value));
		}
		// Vector3
		public static void ShowResult(Vector3 value){
			Vector3 convertion = BitConversion (value), calculation = BitCalculation (value);
			Debug.Log ("Result(Vector3.x) : " + calculation.x + "bit   0x" + convertion.x);
			Debug.Log ("Result(Vector3.y) : " + calculation.y + "bit   0x" + convertion.y);
			Debug.Log ("Result(Vector3.z) : " + calculation.z + "bit   0x" + convertion.z);
		}
	}

}
//******************************************************************//
//******************************************************************//
