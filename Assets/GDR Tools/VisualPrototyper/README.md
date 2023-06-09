# Гайд для утилиты Visual Prototyper

## Решаемые задачи
Вынос связи логики различных скриптов в инспектор на основе `UnityEvent`.

## Основные компоненты
`Visual Prototyper`, `Visual Prototyper Subscriber`. 

## Заложенная логика работы: 
В основе работы GDR SDK лежит использование префабов для уровней. Поэтому для решения задачи прототипирования необходима связь между объектами из префаба и объектами из сцены (например UI).

## Структура компонентов
Оба ключевых компонента состоят из `Visual Action Block`. Каждый такой блок представляет собой последовательность действий, которая будет выполнена по триггеру. Блоки, в свою очередь, непосредственно состоят из `Visual Action`. Действия определяют последовательность действий и их задержку в их исполнении. Задержка отсчитывается относительно предыдущего действия: суммарное время выполнения приведённого блока составит 3 секунды. `Visual Prototyper`, кроме того, определяет используемый триггер.

![](/Images/VisualPrototyper/1.jpg) 

## Триггеры
Представляют из себя Scriptable Object, определяющий тип условия, по которому выполняется очередной блок действий. Имеются триггеры двух типов – отслеживающие нажатие кнопки клавиатуры/мыши и триггеры-таймеры. 
Внутренняя реализация обоих триггеров предполагает использование корутин, поэтому, во избежание неопределённого поведения, от триггеров следует вовремя избавляться с помощью метода `Dispose`. 
Имеется поддержка смены триггера с помощью метода `SwitchTrigger` в компоненте `Visual Prototyper`, но запрещена смена триггера на самого себя.

![](/Images/VisualPrototyper/2.jpg) 

## Использование интерфейса IArgsInjectable и объектов типа Visual Action Args
Недостатком использования `UnityEvents` является невозможность подать в метод количество аргументов, отличное от нуля и единицы.

![](/Images/VisualPrototyper/3.jpg) 

Из трёх приведённых методов только первый и второй могут быть выбраны в инспекторе.

![](/Images/VisualPrototyper/4.jpg) 

Для обхода этого ограничения можно использовать связку интерфейса IArgsInjectable, который предоставляет один метод `InjectArgs(VisualActionArgs)` И ассета типа VisualActionArgs. В конкретной реализации вышеупомянутого метода следует определить порядок типов переменных, которые мы хотим внедрить. Таким образом желаемый порядок действий для метода `TestMethodThree` будет выглядеть как

![](/Images/VisualPrototyper/5.jpg)

![](/Images/VisualPrototyper/6.jpg)

![](/Images/VisualPrototyper/7.jpg)

![](/Images/VisualPrototyper/8.jpg)

Generic метод `GetArgumentValueAs<T>` пытается привести аргумент на соответствующей позиции к запрошенному типу и сохранить его в переменной.

Важно понимать, что метод `InjectArgs` – это некая условность, которая рассчитывает, что на определённой позиции действительно находится аргумент заданного типа. При попытке неудачного каста, например, если мы на первой позиции обещали `string`, а на самом деле положили `int`, будем падать с исключением.

## Порядок работы с объектами типа VisualActionArgs

Сам объект представляет собой контейнер для объектов-аргументов. Все манипуляции с добавлением/удалением аргументов следует проводить через кнопки `Add Argument` и `Remove Arguments`, поскольку они реализуют логику создания/удаления объектов-аргументов, которые хранятся в основном объекте типа `VisualActionArgs`.

![](/Images/VisualPrototyper/9.jpg)

![](/Images/VisualPrototyper/10.jpg)
