class Person {
    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    getName() {
        return this.name;
    }

    setName(name) {
        this.name = name;
    }

    getAge() {
        return this.age;
    }

    setAge(age) {
        this.age = age;
    }

    displayInfo() {
        console.log(`Name: ${this.name}, Age: ${this.age}`);
    }
}

class Student extends Person {
    constructor(name, age, studentId, major) {
        super(name, age);
        this.studentId = studentId;
        this.major = major;
    }

    getStudentId() {
        return this.studentId;
    }

    setStudentId(studentId) {
        this.studentId = studentId;
    }

    getMajor() {
        return this.major;
    }

    setMajor(major) {
        this.major = major;
    }

    displayInfo() {
        super.displayInfo();
        console.log(`Student ID: ${this.studentId}, Major: ${this.major}`);
    }
}

const student = new Student("Alice", 20, "S123456", "Computer Science");

console.log(student.getName());
student.setAge(21);
student.displayInfo();