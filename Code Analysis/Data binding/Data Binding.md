# Data Binding

## Data Binding이란?
logic(data)과 presentation를 분리하고 logic 데이터와 presentation을 동기화시키는 것을 의미한다. 

## Data Binding을 사용하는 이유
프로그램 외부에서 Text값이 변경될 경우, 프로그램 코드가 변경사항으로 인해 에러가 발생할 수 있는 것처럼
디자인 작업은 보통 코드로 작업하지않고 개발 환경에서 직접 디자인을 하기때문에 자주 변경사항이 생길 수 있다.

만약 프로그램에서 A라는 이름의 Text 컴포넌트를 Find 함수를 통해 참조할 것을 가정해보자. 프로그래머 입장에서는 A라는 이름의 오브젝트를   
찾아서 변수에 참조시킬 예정인데, 아티스트가 A에서 B로 이름을 바꾼 경우 프로그래머가 의도한 코드와 미스매치가 발생한다.

이처럼 코드 실행중에 view와 logic data가 결합이되면 즉, 프로그래밍적 선언을 사용하면 view의 변경을 logic data는 반영하지않을 수 있기때문에 문제가 발생한다.
이에 반해 data binding 은 view에서 변경이 일어나도 코드 실행전에 결합이 이루어지기때문에 logic data에 반영시킬 수가 있다.

## Unity의 Data Binding & library
https://bitbucket.org/coeing/data-bind/src/main/
Data binding Asset의 library

## Data Binding 관련 문서(누가 정리해놓은 글)
The "Magic" behind Data Binding (Part 1)
https://www.evernote.com/shard/s722/client/snv?noteGuid=d0d62cea-df58-4ce1-a3eb-edeeba873231&noteKey=022d8d79eb0c5aa6&sn=https%3A%2F%2Fwww.evernote.com%2Fshard%2Fs722%2Fsh%2Fd0d62cea-df58-4ce1-a3eb-edeeba873231%2F022d8d79eb0c5aa6&title=The%2B%2522Magic%2522%2Bbehind%2BData%2BBinding%2B%2528Part%2B1%2529

The "Magic" behind Data Binding (Part 2: Commands and Data Tree)
https://www.evernote.com/shard/s722/client/snv?noteGuid=9f981bcb-b6e6-4612-98a5-245361012239&noteKey=23d5877b474725fd&sn=https%3A%2F%2Fwww.evernote.com%2Fshard%2Fs722%2Fsh%2F9f981bcb-b6e6-4612-98a5-245361012239%2F23d5877b474725fd&title=The%2B%2522Magic%2522%2Bbehind%2BData%2BBinding%2B%2528Part%2B2%253A%2BCommands%2Band%2BData%2BTree%2529

UI Window Management in Unity 5.3 with Data Bind (Part 3)
https://www.evernote.com/shard/s722/client/snv?noteGuid=69437cdd-ac2a-420f-a78b-9992e5e1aacd&noteKey=cc030f17d4e1df99&sn=https%3A%2F%2Fwww.evernote.com%2Fshard%2Fs722%2Fsh%2F69437cdd-ac2a-420f-a78b-9992e5e1aacd%2Fcc030f17d4e1df99&title=UI%2BWindow%2BManagement%2Bin%2BUnity%2B5.3%2Bwith%2BData%2BBind%2B%2528Part%2B3%2529


## Data Binding의 메커니즘

### Unity 환경에서 view와 logic은 어떻게 연결되는가?
view에서 데이터가 변경될경우 logic은 해당 변경사항을 적용시킬 수 있어야됨, view에서 어떠한 이벤트가 발생했을때 logic에서 이를 처리할 수 있어야함
logic은 view에게 데이터를 제공하고 logic에서 데이터가 변경될때 view를 동기화시킬 수 있어야함
-> view의 데이터 변경 명령을 어떻게 logic 측에 알릴 것인가?
-> view의 오브젝트와 logic의 변수를 어떻게 바인딩할 것인가?


###  

## Data Binding의 프레임 워크

### Data Node
### Tree
### Context
#### 경로 접근
#### 

### Property
#### Wrapper 클래스
#### Event

### Reflection
### TypeinfoUtils
### 



## 새로이 알게된 것

### ?? 연산자
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator

(함수 1) ?? (함수 2) -> 함수 1이 null이 아니라면 함수 1을 반환하고 함수 1이 null이라면 함수 2를 반환한다.

### Enumerable.FirstOrDefault
https://docs.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.firstordefault?view=net-6.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0)))

FirstOrDefault는 Enumerable에서 첫번째 요소를 반환하고 만약 요소가 존재하지않으면 Default값을 반환한다. 원하는 Default값을 지정하고 반환시키는 것이 가능하다.   
반환형은 T이다.  
Enumrable.FirstOrDefault(임시 변수명 => (임시 변수명을 포함한 조건식)); -> 여기서 임시 변수명은 Enumrable의 element에 해당하며 조건식에 부합하는 데이터를 종합해서 
그중 첫번째 요소를 반환한다.
ex) names.FirstOrDefault(name => name.Length > 20);
길이가 20자가 넘는 이름을 종합하고 그 중 첫번째(먼저 찾은) 이름을 반환한다. 이때 존재하지않으면 기본값(Default)를 반환한다.
