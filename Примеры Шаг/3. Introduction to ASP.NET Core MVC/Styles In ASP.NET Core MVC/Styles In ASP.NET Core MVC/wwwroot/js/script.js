(() => {
    const createButton = document.querySelector('.request__button');
    const todoCards = document.querySelector('.tasks');
    const inputName = document.querySelector('.request__input-name');
    const inputTittle = document.querySelector('.request__input-tittle');
    let counter = 1;

    createButton.addEventListener('click', () =>{
        if (inputName.value === '') {
            alert('You must write name of task');
        }else if(inputTittle.value === ''){
            alert('You must write tittle');
        }else{
            const card = document.createElement('li');
            const cardName = document.createElement('span');
            const cardTittle = document.createElement('p');
            const cardCheckBox = document.createElement('input');
            const labelCheckBox = document.createElement('label');
            const deleteButton = document.createElement('button');
    
            card.classList.toggle('card');
            cardTittle.classList.toggle('card__tittle');
            cardName.classList.toggle('card__name');
            cardCheckBox.classList.toggle('card__checkbox');
            cardCheckBox.id = `card__checkbox-${counter}`;
            labelCheckBox.classList.toggle('card__checkbox');
            labelCheckBox.setAttribute('for', `card__checkbox-${counter}`);
            cardCheckBox.type = 'checkbox';
            deleteButton.classList.toggle('card__button');
            counter++;

            todoCards.append(card);
    
            card.append(cardCheckBox);
            card.append(labelCheckBox);
    
            cardName.innerText = inputName.value;
            card.append(cardName);
            inputName.value = '';
    
            cardTittle.innerText = inputTittle.value;
            card.append(cardTittle);
            inputTittle.value = '';
    
            card.append(deleteButton);
            deleteButton.innerText = 'Delete';
    
            cardCheckBox.addEventListener('click', () => {
                if(cardCheckBox.checked){
                    cardName.classList.add('card__name--done');
                    cardTittle.classList.add('card__tittle--done');
                }else{
                    cardName.classList.remove('card__name--done');
                    cardTittle.classList.remove('card__tittle--done');
                };
            })
    
            deleteButton.addEventListener('click', () => {
                if (cardCheckBox.checked){
                    card.remove();
                };
            });
        };
    });

})();
