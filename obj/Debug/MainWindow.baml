   M S B A M L   `   `   `  ����D  @ROMLoader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ROMLoader.ViewModelsROMLoader.ViewModels  5   	   %clr-namespace:ROMLoader	ROMLoader  5   	       ROMLoader.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1vm"clr-namespace:ROMLoader.ViewModels     5   	   )localclr-namespace:ROMLoader     5   	     � Title$  Coal Loading System��5	   	   $	��600��6%   $
��1000��62      MainViewModel � DataContext# �   5
   	    � 	Resources 5      (  "ROMLoader.ViewModels.MainViewModel��%   &	          5      6   5      .��5      �� 
�������$��1q�6%   ._�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� $��0��6   
�������$��10q�6<   ._�5      �� .+�5      �� $��18(�6    $
��Bold"�6.    �� TextAlignment$ Center=�6@    �� Padding$ 8q�6W   Blend Cycle6p   �� 5      $��8, 0, 8 , 0q�6    J� IsHitTestVisible .  ��64   �� *
BlendCycle+ �� SelectedIndex 5      �� *	CoalIndex+6a   5      5       �� 5#      $��1��6   
�������$��10q�6<   ._�5$      �� .+�5%      �� $��18(�6    $
��Bold"�6.   $ Center=�6@   $ 8q�6W   
Stockpiles6o   &   System.Windows.Controls.DataGrid  5&      $��8, 0, 8 , 0q�6      AutoGenerateColumns .  ��64   �� *
Stockpiles+	   Columns	 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Stockpile Number��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 1.2* 6o   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6K   �� *StockPileNumber+6o     5)      $

 Coal��61   $ 2* 6Y    6?   �� *Coal+6Y   5+      5,      5-      � 51      $��2��6    � RowDefinitions 52      �� 53      $��3* �6$   �� 54      $��* �6$   .+�58      �� $��0��6   
�������$��10q�6=   ._�59      �� .+�5:      �� $��18(�6$   $
��Bold"�62   $ Center=�6D   $ 8q�6[   Load Schedule6v   �� 5;      $ 8,0,0,8q�6$   Incoming Trucks:6H     5<      $	��250��6#   $��8, 0, 8 , 0q�60    .     CanUserAddRows .     
IsReadOnly . ��6E   �� *CoalMovements+	 5>        5?   "   $
 
Truck Name��65   $ 1* 6d    6I   �� *Truck+6d     5@   "   $

 Coal��65   $ * 6^    6C   �� *Coal+6^     5A   "   $
 Arrival Time��65   $ 2* 6p    6K   �� *DateTimeArrival+6p   5D      �� 5E       �� Orientation$ 
Horizontal=�6%   .+�5F      �� $��8, 10, 10, 0q�6(   Load:6E   �� 5G      $��0, 10, 0, 0q�6&   $	��150��6;    �� 
IsReadOnly . ��6G   �� *LoadingCoal+6z   �� 5H      $��8, 10, 8, 0q�6%   $��93��6:   ��6E   �� *LoadCoalCommand+.��6i   	Load Coal6t   5I      5K      5M      �� 5P      $��1��6   
�������$��10q�6=   ._�5Q      �� .+�5R      �� $��18(�6$   $
��Bold"�62   $ Center=�6D   $ 8q�6[   	Load Time6r   � 5T       5U      �� 5V   "   $	��160 �63   �� 5W   "   $	��100 �63    5Z      �� 5[   "   $��* �60   �� 5\   "   $��* �60   .+�5_      �� $��0��6(   $��0��65   $ 10, 5, 0, 0q�6E   
Load time:6i   �� 5`      $��1��6(   $��0��65   $ 10, 5, 0, 5q�6E   Maximum truck wait time:6u   �� 5b      $ 
Horizontal=�6)   $��0��6B   $��1��6O   .+�5c   "   �� $��5,5,0,5q�6+   $��50��6<    . ��6G   �� *
LoadTime+6a   �� 5d   "   $��5,5,0,5q�6*   $��˄��6;   $��20��6G   ��6R   �� *IncreaseLoadTimeCommand+�� 5e   "   $��0,5,0,5q�6*   $��˅��6<   $��20��6H   ��6S   �� *DecreaseLoadTimeCommand+5f      �� 5h      $ 
Horizontal=�6)   $��1��6B   $��1��6O   .+�5i   "   �� $��5,5,0,5q�6*   $��50��6;    . ��6G   �� *MaxWaitTime+6e   �� 5j   "   $��5,5,0,5q�6*   $��˄��6=   $��20��6I   ��6T   �� *IncreaseMaxWaitTimeCommand+�� 5k   "   $��0,5,0,5q�6*   $��˅��6<   $��20��6H   ��6S   �� *DecreaseMaxWaitTimeCommand+5l      5m      5n      5o      5p      5q      5r      5s      