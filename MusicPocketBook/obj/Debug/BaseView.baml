   M S B A M L   `   `   `  ����J  FMusicPocketBook, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:MusicPocketBookMusicPocketBook  5   	   G(clr-namespace:MusicPocketBook.ViewModelsMusicPocketBook.ViewModels  5   	   =#clr-namespace:MusicPocketBook.ViewsMusicPocketBook.Views  5   	       MusicPocketBook.BaseView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:MusicPocketBook     5   	   ?
viewModels(clr-namespace:MusicPocketBook.ViewModels     5   	   5views#clr-namespace:MusicPocketBook.Views     5   	     � Title$  The Musician's Pocketbook��5
   	   $	��400��6+   $	��825��68    � 
ResizeMode$ NoResize=�5   	    � 	Resources 5       �� DataType.  (MusicPocketBook.ViewModels.HomeViewModel$   MusicPocketBook.Views.HomeView0  *MusicPocketBook.ViewModels.KeyGenViewModel&    MusicPocketBook.Views.KeyGenView/  )MusicPocketBook.ViewModels.ChordViewModel%   MusicPocketBook.Views.ChordView   ButtonStyle �� 
TargetType �� Property �� Value��%�  (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)&	  �     �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   " ��6$   .�5      ��  � ��$
 null)�6+   �� 5       � ��$ 15(�6)   �� 5       � ��$
 Thin"�6+   �� 5       � ��
 �����6+   �� 5       � ��
 ��iA�6+   5      5      .��5       � 
�������.+�5#   
   ]� .+�5$      �� $
��Left=�6   
����iA�$��0,0,.5,0q�6C   $	��110��6^   ._�5%      �� ��6   �� *PageViewModels+��5&      �� 5'      .R�5(      �� #��[  5+   !   $��0,0,0,0q�5,   !   $	��121��5-   !   $��0,0,0.5,0.5q�5.   !   
����iA���5(   !   �� *vmName+��5)   !   �� *DataContext.ChangePageCommand+ �� RelativeSource ��  �� AncestorType" ���5*   !   �� 5/   !   51      53      54      �� 56      ��6   �� *CurrentPageViewModel+57      58      59      