# data binding 시스템을 혼자서 구축할 수 있는지 조사해보기
- data binding article 읽고 어떤 컨셉으로 프로그래밍을 했는지 정리해보기
- 컨셉을 가지고 대략적인 인터페이스 짜보기



## data binding

presentation <-----> logic
presentation은 logic 측에 값이 변경되었음을 알릴 수 있어야함
logic은 presentation쪽에 값을 전달할 수 있어야함 또한 갱신도 필요함

property 클래스 -> data에 대한 wrapper 클래스 -> data 값이 변경되었을때 이를 logic(프로그램)쪽에 알리기위해서 구현
-> 이벤트 핸들러를 선언해서 값이 변경되었을때 특정 액션이 실행됨

Context는 property의 집합이며 프레젠테이션 측에서 프로퍼티를 사용할 수 있도록 만듦
데이터 트리 형식으로 데이터값을 저장함 -> 노드가 존재하며 해당 노드에는 메타 파일이 존재(이름, 자식 노드, 필드값, 타입 정보, 값이 변경될때의 이벤트 핸들러)
Context는 경로형식으로 정의되어있으며 해당 프로퍼티를 찾을때는 경로를 추적하여 찾는것이가능하다.
Context는 RegisterListener(), RemoveListener(), Setvalue()가 핵심 메소드를 가지며 Setvalue로 프로퍼티값을 변경한다.
RegisterListener(), RemoveListener()는 지정된 경로의 값이 변경되었을때 파라메터로 전달되는 콜백을 호출하도록 한다.


## Concept
presentation 쪽에서는 사용자가 경로를 지정하고 그 경로를 추적하여 Context로부터 값을 받아와야함
그리고 그 값을 UI 종류에 따라 Script에서 값을 세팅해야함

Logic 측에서는 먼저 Property 클래스를 선언하고 이 클래스를 상속받아서 분류화작업을 해야함
ex - TextProperty , ButtonProperty ...
Property 클래스에서 핵심적인 요구는 값이 변경되었을때 이 부분을 알아차릴 수 있는 이벤트 핸들러임 -> 왜냐하면 Context에서 값이 바뀜을 전달하고 Property 핸들러가 이를 수신하면
Property 내부에 있는 값을 변경된 값으로 바꿔야하기때문
-> 구체적으로는 경로에 해당하는 프로퍼티 노드의 이벤트를 context의 RegisterListener()를 통해 가입시켜 경로로 탐색한 후 값이 변경되면 해당 값으로 값을 바꾸도록 함

Context는 Property 값들을 트리 형식으로 저장하고 외부에서 경로로 탐색할 수 있도록 준비해야함

## 다음에 할것
2. Unity에서 키보드 입력을 받을때 입력에 대한 적용대상을 레이어로 계층화시키고 키보드 입력을 받을때 하나의 UI만 입력을 받도록 하기
3. 운영체제 정리 및 강의 2개 듣기

