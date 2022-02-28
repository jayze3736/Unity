readonly
readonly는 const 처럼 필드값이 바뀌지않게 하기위해 사용한다.
const는 컴파일 시점부터 필드값을 고정시키지만
readonly는 런타임 시점부터 필드값을 고정시키며 생성자로 필드값을 1번 초기화했을때만 값 변경이 가능하므로
객체에서 1번만 초기화하도록 하고 그 이후는 값이 변하지않아야하는 경우에 readonly를 사용한다.
readonly는 변수 제한을 주어 프로그램의 질을 높인다.

https://www.dotnetperls.com/readonly
