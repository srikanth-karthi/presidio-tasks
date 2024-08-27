using System;

public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
}

public class LinkedList
{
    public ListNode head;

    public void Add(int val)
    {
        ListNode newNode = new ListNode(val);
        if (head == null)
        {
            head = newNode;
            return;
        }

        ListNode current = head;
        while (current.next != null)
        {
            current = current.next;
        }
        current.next = newNode;
    }

    public bool HasCycle()
    {
        if (head == null || head.next == null)
        {
            return false;
        }

        ListNode slow = head;
        ListNode fast = head.next;

        while (slow != fast)
        {
            if (fast == null || fast.next == null)
            {
                return false;
            }
            slow = slow.next;
            fast = fast.next.next;
        }

        return true;
    }
}


