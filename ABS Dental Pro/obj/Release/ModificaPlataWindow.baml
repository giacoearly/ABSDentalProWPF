   M S B A M L   `   `   `  ���� I  EABS Dental Pro, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABS_Dental_ProABS_Dental_Pro  (    "ABS_Dental_Pro.ModificaPlataWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:ABS_Dental_Pro       � Title$  Modifică plată�� � Icon$ Images/edit.png��$	��380��$	��550�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=� � 	Resources    myLabels �� 
TargetType �� Property �� Value  myTextBoxes  myComboBoxes  myTextBlocks  myTextBlocks2  styleCalendar&   System.Windows.Controls.Calendar	 �� 
TargetType5  /System.Windows.Controls.Primitives.CalendarItem
  Name��%�  &	        &	 =     &	 �     &	 �     &	 �     &	 �     �� " ��.���  � ��$ 16(���  � ��$ Bottom=��� " ��.���  � ��$ 16(���  � ��
 �������  � ��$ Bottom=��� " ��.���  � ��$ 16(��� " ��.���  � ��$ 18(��� " ��.���  � ��$ 20(��� "  .���  � �� �� "	  .R�(� $	��300��$	��300��.��  $
 PART_CalendarItem��#��z��#��z��#��z��.����  �� Orientation$ Vertical=�$��	10,10,0,0q�.+�� $��370.8�� � ColumnDefinitions �� $	��200 ���  � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� .+��� $��Medic��$��0��#��[ $��0���� -    ��Name$ cbMedic��$��1��$��0��#��[ $��0q�$��28��$	��253��$
��Left=�$	��Top=��� $��Total de plătit��$��0��#��[ $��1���� -    ��Name$ tbTotal��$��1��#��[ $��Center=�$	��Top=�$��0q�$��80��$
��Left=�$��1���� $��Total plătit��$��2��$��0��#��[ �� -   $ tbTransa��$��2��$��1��#��[ $	��Top=�$��80��$
��Left=�$��Center=��� $��Rest de plătit��$��3��$��0��#��[ �� -   $ tbRest��
���  ��$
��Bold"�$��3��$��1��#��[ $	��Top=�$��80��$
��Left=��� $
��Data��$��4��$��0��#��[ �� $��	Descriere��$��5��$��0��#��[ �� -   $ tbDescriere��$��5��$��1��#��[ $	��Top=�$
��Left=�$	��300��$��Center=�$��0q��� -    ��Name$ btnModificaPlata�� � ToolTip$ Modifică ultima plată��$��6��$��0��$��2��$��0,30,0,0q�$	��140��$��40��$��Center=�$	��Top=�.���� $ 
Horizontal=�.+��� $��Images/edit.png��$
��Left=�$��Center=�$��0,0,20,0q� J� RenderTransformOrigin$ 0.5,0.5&�$��40��$��40���� $��	Modifică��$��16(�$��Center=�$��Right=�$��0q�(  "System.Windows.Controls.DatePicker  -     Name$ 
datePicker��   CalendarStyle# [ $
��Left=�$	��120��$��0q�$��4��$��1��$��25��$	��Top=�