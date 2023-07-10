# ML_voice

<br><h2>1. 지도학습</h2>


입력과 결과값이 주어진 훈련 데이터(75%)와 결과값만 없는 테스트 데이터(25%)로 지도학습한다.

- import pandas(데이터 처리)


총 2373명에 대한 22개 주파수 feature이다. 

![image](https://user-images.githubusercontent.com/82012857/176307604-87a4246d-86fe-4c26-a9bd-46e6deb94c96.png)


<br><h2> 2. feature 선정</h2>


정확한 성별 구별을 위한 feature를 고르기 위해 필요한 파이썬 라이브러리를 불러온다.

- import matplotlib.pyplot(그래프 시각화)

- import seaborn(데이터 분포)


facet 함수를 사용하여 그래프로 성별에 따른 주파수를 확인한다.

이때  파란색은 남자이고 빨간색은 여자인데, 눈으로 봤을 때 겹치지 않는 것으로 고른다.


ex.

![image](https://user-images.githubusercontent.com/82012857/176307799-f499f6a1-745b-455b-8385-12c7042fb11d.png)

                                                        선정 O

![image](https://user-images.githubusercontent.com/82012857/176307806-0be25a71-93bb-4194-8e61-3cf7576ac9af.png)

                                                        선정 X


minfun의 경우 구간에 따라 주파수가 반대의 성질을 띄었기 때문에 포함시켰다.

![image](https://user-images.githubusercontent.com/82012857/176307935-cc079b43-19eb-4941-963c-5f5361afc352.png)

                                                        선정 O

![image](https://user-images.githubusercontent.com/82012857/176308061-b03cbe33-153d-4e5d-851f-4067b0e5c4b0.png)


그렇게 5가지가 추려졌다.



<br><h2> 3. 가지치기</h2>


그래프가 아닌 값을 통해 판단하기 위해 다이어그램을 확인해 보았다.

 5개의 feature 중에서 성별을 분류하는 meanfun, IQR 값에 대한 의사결정트리는 이러한 결과가 나왔다.

![image](https://user-images.githubusercontent.com/82012857/176308130-e4ea52a5-b68e-469c-9777-b76b14129a02.png)

                                                        decision tree


카트 다이어그램으로 핵심만 보면,

meanfun 값이 140hz가 넘으면 웬만해서 여자 목소리이지만, 140hz가 넘지 않는 여자가 있을 수도 있기 때문에 140hz 이하 중에서 IQR 값이 0.07보다 작거나 같으면 여자로 분류하여 정확도를 높인다는 뜻이다.

![image](https://user-images.githubusercontent.com/82012857/176308180-e0b72748-bfac-454e-a600-68be2c3c2f11.png)

                                                        cart diagram


<br><h2> 4. 중간 점검</h2>


사실 굳이 할 필요는 없지만 LinearRegression을 통해 중간 점검을 해보았다.

여자의 경우 1.0~1.3이고 남자의 경우 1.8 ~ 2.2의 값이 나왔다.

![image](https://user-images.githubusercontent.com/82012857/176308224-cc696964-3a3d-47c9-92a2-b1e3da948add.png)

                                                        Linear Regression


<br><h2> 5. 성능 평가</h2>


sklearn에서 제공하는 다른 모델들을 적용해 보았다.

![image](https://user-images.githubusercontent.com/82012857/176308356-12f7748d-e4bc-46f5-bcfb-066047d49510.png)


대부분 96~97%의 정확도를 보였는데, 이 중 SVM을 학습시켰다.

그랬더니 LinearRegression에서의 소수점을 보이며 불안전한 모습과는 달리 SVM 모델에서는 1과 2로 정확하게 나뉘었다.

![image](https://user-images.githubusercontent.com/82012857/176308380-e00458ba-3918-4010-9300-5237291e4e6c.png)


                                                        SVM


<br><h2> 6. 목소리 주파수 분석</h2>


이번에는 직접 녹음한 목소리의 성별을 구별해 보기로 하였다. 

남자와 여자 각각 하나씩 목소리 샘플을 R studio의 warbleR, seewave, tuneR 패키지를 이용하여 생체 음향을 분석하고 22가지 음향 매개변수를 측정함으로써 테스트 데이터 셋을 만들었다.

![image](https://user-images.githubusercontent.com/82012857/176308439-ec3cd670-560c-46da-a87d-5beb8ba2593d.png)


<br><h2> 7. 결과</h2>


측정한 데이터를 훈련한 모델로 실험해 보았는데, 결과를 확인해 보니 성공적으로 분류되었다.

![image](https://user-images.githubusercontent.com/82012857/176308473-15c0ee58-1ed7-49de-b151-1d7561891892.png)

<br><hr>

<br><h2> 출처</h2>

데이터 셋

https://stickie.tistory.com/51?category=746224


http://www.primaryobjects.com/2016/06/22/identifying-the-gender-of-a-voice-using-machine-learning/


cart diagram

https://www.kaggle.com/primaryobjects/voicegender


R 패키지

https://www.rdocumentation.org/packages/warbleR/versions/1.1.2/topics/specan
