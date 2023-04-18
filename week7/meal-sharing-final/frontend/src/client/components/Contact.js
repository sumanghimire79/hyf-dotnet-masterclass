import React from 'react';

export function Contact() {
  return (
    <div className="container">
      <p>Telefon : 27859433 </p>
      <p>Address : HYF-engavevej-80c</p>
      {
        <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d4501.158814572418!2d12.540374!3d55.661524!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46525383237655ff%3A0x50853b467dbe5e2c!2sEnghavevej%2080%2C%202450%20K%C3%B8benhavn!5e0!3m2!1sda!2sdk!4v1650740190793!5m2!1sda!2sdk"></iframe>
      }

      <form>
        <label htmlFor="fname">First Name</label>
        <input type="text" name="firstname" placeholder="Your name.." />

        <label htmlFor="lname">Last Name</label>
        <input type="text" name="lastname" placeholder="Your last name.." />

        <label htmlFor="country">Country</label>
        <select id="country" name="country">
          <option value="denmark">Denmark</option>
          <option value="sweden">Sweden</option>
          <option value="norway">Norway</option>
          <option value="other">Other</option>
        </select>

        <label htmlFor="message">Message</label>

        <textarea
          type="text"
          name="message"
          placeholder="Write message.."
        ></textarea>

        <input type="submit" />
      </form>
    </div>
  );
}
