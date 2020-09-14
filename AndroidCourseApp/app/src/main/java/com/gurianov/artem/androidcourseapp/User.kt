package com.gurianov.artem.androidcourseapp

class User(val firstName: String, val lastName: String, val email: String,
           val password: String, val photoId: Int
) {

    companion object {
        var userPresets = listOf(
            User("Example", "Example", "example@example.com", "Example0", R.drawable.example),
            User("Артем", "Гурьянов", "artemgur01@gmail.com", "Artemgur01", R.drawable.artemgur),
            User("Ирина", "Шахова", "is@it.kfu.ru", "Qwerty2", R.drawable.profile2)
        )
    }
}