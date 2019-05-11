Palmtree 数値計算ライブラリ 相互運用機能 v1.0.0
=============================================
.NET Framework で数値計算を行うためのライブラリです。
長精度の整数や有理数の演算に特化していますので、機能的にはあまり多くありません。  
なお、SFMTによる乱数生成もサポートしています。
 
動作環境
--------
.NET Framework 4.5以降が動作する x86/x64 CPUのコンピュータが必要です。

主な機能について
---------------
### `Palmtree.Math.UBigInt`型と`System.Numerics.BigInteger`型の相互型変換機能 ###
`Palmtree.Math.UBigInt`型から`System.Numerics.BigInteger`型へ、および
`Palmtree.Math.UBigInt`型から`System.Numerics.BigInteger`型への型変換機能を提供します。
 
更新履歴
--------
### v1.0.0 ###
* 正式公開

プロジェクトURL
--------------
https://github.com/rougemeilland/Palmtree.Math.Interop

著作権
------
&copy; 2019 Palmtree Software

ライセンス
----------
Licensed under the [MIT License][mit].
 
[MIT]: http://www.opensource.org/licenses/mit-license.php
