/*
Входные данные: целые знаковые числа x и y.
Выходные данные:
	1) дополнительные коды для входных чисел x', y'.
	2) разность чисел x' и y' согласно правилам арифметики чисел, представленных дополнительным кодом.
	3) знаковое представление полученного результата вычитания.
Рекомендации:
1) выполнить преобразование входных знаковых чисел в дополнительный код;
2) реализовать алгоритм вычитания чисел в дополнительном коде (если уменьшаемое меньше вычитаемого, то к нему прибавить 10000);
3) выполнить преобразование полученного дополнительного кода в знаковое число.
*/

#include <stdio.h>
#include <stdlib.h> 

int ABS(int i)
{
	return (((i >= 0) - (i < 0)) * i);
}

int main()
{
	int x = 0, y = 0, Sum = 0, xOLD = 0, yOLD = 0;
	
	printf("Введите X и Y: ");
	scanf("%d %d", &x, &y);
	
	if (x > 4999 || x <= -5000 || y > 4999 || y <= -5000)
	{
		printf("Ошибка данных\n");
		exit(1);
	}

	//Данные корректны
	if (x >= 0)
		xOLD = x;
	if (y >= 0)
		yOLD = y;

	if (x < 0)
		xOLD = 10000 - ABS(x);
	if (y < 0)
		yOLD = 10000 - ABS(y);

	printf("\tДополнительные коды для входных чисел: \t\tX`=%u;\tY`=%u\n", xOLD, yOLD);

	if (xOLD < yOLD)
		xOLD = xOLD + 10000;

	Sum = xOLD - 10000 + yOLD;

	if (Sum >= 10000)
		Sum -= 10000;
	if (Sum < 0)
		Sum += 10000;
	if ((Sum >= 5000) && (x > 0) && (y > 0))
		Sum = 4999;
			
	printf("\tРазность чисел (X`-Y`) в дополнительном коде: \t%d\n", Sum);
			
	if ((0 <= Sum) && (Sum < 5000))
		printf("\tЗнаковое представление числа (X`-Y`): \t\t%d\n", Sum);
	if ((Sum >= 5000) && (Sum < 10000))
		printf("\tЗнаковое представление числа (X`-Y`): \t\t%d\n", (10000 - Sum) * (-1));
	return 0;
}
