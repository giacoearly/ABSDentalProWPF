   M S B A M L   `   `   `  ����I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  5   	   '    !ABS_Dental_Pro.IstoricPlatiWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABS_Dental_Pro     5   	     � Title$  Istoric plăți��5   	   $	��550��6    � WindowStartupLocation$ CenterScreen=�6,    � ShowInTaskbar .   � 
ResizeMode$ NoResize=�6g    � Icon$" /Images/if_wallet_286672.ico��5	      $	��700��64    � 	Resources 5
         myLabels �� 
TargetType �� Property �� Value  myTextBoxes  myTextBlocks2��%�  &	        &	 z     &	     �� 5   
   " ��6!   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��$ Bottom=�62   6C   5      �� 5   
   " ��6$   .�5      ��  � ��$ 16(�6)   66   �� 5       � ��
 �����6;   �� 5       � ��$ Bottom=�62   6C   5      �� 5   
   " ��6&   .�5      ��  � ��$ 16(�6)   66   5      5      .��5      �� .+�5   
   �� .+�5+      �� -   5        	 ��Name$	 tbTotal��5+      $��Total plăți:��6)   $��24(�6=   $��
25,10,-5,0q�6K   5,      &   System.Windows.Controls.DataGrid  5-   
   -   5        $��dataGridIstorii��5-      
   SelectionMode$
 Single=�6S      AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly . ��. $
��Auto=�50      $
��Auto=�51      $	��430��6Q   $��0,20,0,0q�52   j   $��14(�6|   ��5-   *   �� *listaIstoriiProp+ � DataContext 52      ��  �� RelativeSource ��  �� AncestorType$ WindowL�   Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ Medic��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Medic+  55      $ Tranșa��6%    65   �� *Transa+  56      $
 Data��6%    63   �� *Data+  57      $ 	Descriere��6%    68   �� *	Descriere+59      5:      5=      