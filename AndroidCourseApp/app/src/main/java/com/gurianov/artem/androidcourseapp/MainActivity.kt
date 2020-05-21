package com.gurianov.artem.androidcourseapp

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    companion object {
        val userId = "userId"
        val prefs = "preferences"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val preferences = getSharedPreferences(prefs, Context.MODE_PRIVATE)
        if (preferences.contains(userId))
            logIn(preferences.getInt(userId, -1))
        setContentView(R.layout.activity_main)
    }

    fun signInButtonClick(view: View) {
        val email = email_input.text
        val isEmailValid = android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()
        if (!isEmailValid) {
            Toast.makeText(this, R.string.invalid_email, Toast.LENGTH_SHORT).show()
            return
        }
        tryLogIn()
    }

    private fun isPasswordValid(password: String): Boolean {
        var containsCapital = false
        var containsSmall = false
        var containsNumber = false
        for (char in password)
            when {
                char.isDigit() -> containsNumber = true
                char.isUpperCase() -> containsCapital = true
                char.isLowerCase() -> containsSmall = true
            }
        if (!(containsNumber && containsCapital && containsSmall)) {
            Toast.makeText(this, R.string.invalid_password, Toast.LENGTH_SHORT).show()
            return false
        }
        return true
    }

    private fun tryLogIn(){
        val email = email_input.text.toString()
        val password = password_input.text.toString()
        for (i in 0 until User.userPresets.count())
            if (User.userPresets[i].email == email) {
                if (User.userPresets[i].password == password) {
                    val preferences = getSharedPreferences(prefs, Context.MODE_PRIVATE)
                    val editor = preferences.edit()
                    editor.putInt(userId, i)
                    editor.apply()
                    logIn(i)
                }
                else
                    if (isPasswordValid(password))
                        Toast.makeText(this, R.string.incorrect_password, Toast.LENGTH_SHORT).show()
                return
            }
        Toast.makeText(this, R.string.incorrect_email, Toast.LENGTH_SHORT).show()
    }

    private fun logIn(userIndex: Int){
        val intent = Intent(this, ProfileActivity::class.java)
        intent.putExtra(userId, userIndex)
        startActivity(intent)
        finish()
    }
}
