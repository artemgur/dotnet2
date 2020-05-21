package com.gurianov.artem.androidcourseapp

import android.content.Context
import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import kotlinx.android.synthetic.main.activity_profile.*

class ProfileActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        val user =  User.userPresets[intent.getIntExtra(MainActivity.userId, -1)]
        setContentView(R.layout.activity_profile)
        email_view.text = user.email
        name_view.text = user.firstName
        last_name_view.text = user.lastName
        image_view.setImageResource(user.photoId)
    }

    fun exitClick(view: View) {
        val editor = getSharedPreferences(MainActivity.prefs, Context.MODE_PRIVATE).edit()
        editor.remove(MainActivity.userId)
        editor.apply()
        val intent = Intent(this, MainActivity::class.java)
        startActivity(intent)
        finish()
    }
}
